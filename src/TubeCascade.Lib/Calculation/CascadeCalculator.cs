using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;
using TubeCascade.ElementList;
using TubeCascade.Models;
using TubeCascade.Primitives;

using static TubeCascade.Calculation.Constants;
// ReSharper disable UseDeconstruction
// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

[assembly: InternalsVisibleTo("TubeCascade.Tests")]

namespace TubeCascade.Calculation;

/// <inheritdoc />
public class CascadeCalculator : ICascadeCalculator
{
	private readonly IResistors resistors;
	private readonly ICapacitors capacitors;

	public CascadeCalculator(IResistors resistors, ICapacitors capacitors)
	{
		this.resistors  = resistors;
		this.capacitors = capacitors;
	}

	/// <inheritdoc />
	public TriodeAmpCascade CalculateCascade(CascadeInputData inputData)
	{
		var (_, tube, _) = inputData;
		var cascadeDraft = CascadeDraft(tube);

		var anodeResistorDraft = new Resistor(02 * tube.InternalResistance, 1.5 * tube.NominalVoltage, default);
		var tubeRestPoint = TubeRestPoint(tube, anodeResistorDraft);

		var anodeResistorPower = new Power((tube.NominalVoltage - tubeRestPoint.Voltage) * tubeRestPoint.Current);
		var anodeResistor = anodeResistorDraft with { MaxDissipatedPower = anodeResistorPower };

		var cathodeResistor = new Resistor(
			Nominal: -1 * tubeRestPoint.Voltage,
			MaxVoltage: tube.NominalVoltage,
			MaxDissipatedPower: new Power(2));

		var (cathodeCapacitor, isolationCapacitor) = CalculateCapacitors(inputData, cathodeResistor);

		return WithStandardValues(cascadeDraft with
		{
			AnodeResistor      = anodeResistor,
			CathodeResistor    = cathodeResistor,
			CathodeCapacitor   = cathodeCapacitor,
			IsolationCapacitor = isolationCapacitor
		});
	}

	/// <summary>
	/// Create draft of triode cascade.
	/// </summary>
	private static TriodeAmpCascade CascadeDraft(VacuumTriode tube)
		=> new()
		{
			Tube             = tube,
			LeakResistor     = new Resistor(10 * Mega, new Voltage(10), 0.5),
			SpuriousResistor = new Resistor(10 * Kilo, new Voltage(10), 0.5),
		};

	/// <summary>
	/// Calculate nominal values for capacitors.
	/// </summary>
	private static (Capacitor CathodeCapacitor, Capacitor IsolationCapacitor) CalculateCapacitors(
		CascadeInputData inputData,
		Resistor cathodeResistor)
	{
		var (inputSignal, tube, nextCascadeResistance) = inputData;

		var cathodeCapacity =
			new Capacity(10 / (2 * Math.PI * inputSignal.FrequencyRange.LeftBound * cathodeResistor.Nominal)); 
		var cathodeCapacitor = new Capacitor(
			Nominal: cathodeCapacity.Limit(new(10 * Micro), new(100 * Micro)),
			MaxVoltage: tube.NominalVoltage);

		var isolationCapacity =
			new Capacity(10 / (2 * Math.PI * inputSignal.FrequencyRange.LeftBound * nextCascadeResistance));
		var isolationCapacitor = new Capacitor(
			Nominal: isolationCapacity.Limit(new(50 * Micro), new(250 * Micro)),
			MaxVoltage: tube.NominalVoltage);

		return (CathodeCapacitor: cathodeCapacitor, IsolationCapacitor: isolationCapacitor);
	}

	/// <summary>
	/// Get rest point for <paramref name="tube"/>.
	/// </summary>
	private static (Voltage Voltage, Current Current) TubeRestPoint(
		VacuumTriode tube,
		Resistor anodeResistorDraft)
	{
		var loadLine = BuildLoadLine(tube, anodeResistorDraft);

		var (_, restVoltageToCurrentRatio) = tube
			.AnodeCharacteristics
			.Where(characteristic => characteristic.GridAnodeVoltage.Value < 0)
			.MaxBy(characteristic => characteristic.GridAnodeVoltage);

		var ratioLine = (Tangent: 1 / restVoltageToCurrentRatio, Constant: 0);

		var coefficient = ratioLine.Tangent > loadLine.Tangent ? 1 : -1;
		var voltage = new Voltage(coefficient * 120).Limit(new(100), tube.NominalVoltage);
		var current = new Current(5 * Milli).Limit(new(2 * Milli), new(20 * Milli));
		return (Voltage: voltage, Current: current);
	}

	/// <summary>
	/// Build tube's load line represented as a linear equation.
	/// </summary>
	private static (double Tangent, double Constant) BuildLoadLine(
		VacuumTriode tube,
		Resistor anodeResistor)
	{
		var (nominalVoltage, _, _) = tube;
		var lockedTubePoint = (X: 0, Y:nominalVoltage.Value);
		var lockedAnodeCathodePoint = (X: nominalVoltage.Value / anodeResistor.Nominal.Value, Y: 0);

		var tangent = (lockedTubePoint.Y - lockedAnodeCathodePoint.Y) / (lockedTubePoint.X - lockedAnodeCathodePoint.Y);
		return (Tangent: tangent, Constant: lockedTubePoint.Y);
	}

	/// <summary>
	/// Properties of <see cref="TriodeAmpCascade"/> with type derived from <see cref="IElementBase"/>.
	/// </summary>
	private static readonly IReadOnlyCollection<PropertyInfo> cascadeElementProps =
		typeof(TriodeAmpCascade)
			.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.Where(prop => prop.DeclaringType!.IsAssignableTo(typeof(IElementBase)))
			.ToArray();

	/// <summary>
	/// Set all nominal values of elements in <paramref name="cascadeDraft"/>
	/// to standard values based on data from <see cref="ICapacitors"/> and <see cref="IResistors"/>.
	/// </summary>
	private TriodeAmpCascade WithStandardValues(TriodeAmpCascade cascadeDraft)
	{
		IElementBase WithStandardNominal(IElementBase draftValue)
			=> draftValue switch
			{
				Capacitor capacitor => capacitors.NearestFromAbove(capacitor),
				Resistor  resistor  => resistors.NearestFromAbove(resistor),
				_ => throw new NotSupportedException($"Element with type '{draftValue.GetType()}' is not supported.")
			};

		var standardElements = cascadeElementProps
			.Select(prop => (
				Property: prop,
				Element: (IElementBase)prop.GetValue(cascadeDraft)!))
			.Select(tuple => (tuple.Property, Element: WithStandardNominal(tuple.Element)))
			.ToImmutableArray();

		var cascadeWithStdElements = cascadeDraft with { };
		standardElements.ForEach(tuple => tuple.Property.SetValue(cascadeWithStdElements, tuple.Element));
		return cascadeWithStdElements;
	}

	/// <inheritdoc />
	public Func<InputSignal, Power> CalculatePower(TriodeAmpCascade triodeAmpCascade)
	{
		throw new NotImplementedException();
	}
}