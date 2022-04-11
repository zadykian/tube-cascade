using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;
using TubeCascade.ElementList;
using TubeCascade.Models;
using TubeCascade.Primitives;

using static TubeCascade.Calculation.Constants;

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
		var (inputSignal, tube, nextCascadeInputResistance) = inputData;

		var cascadeDraft = new TriodeAmpCascade
		{
			Tube             = tube,
			LeakResistor     = new Resistor(10 * Mega, new Voltage(10), 0.5),
			SpuriousResistor = new Resistor(10 * Kilo, new Voltage(10), 0.5),
		};
		
		// AnodeResistor    = new Resistor(02 * tube.InternalResistance, 1.5 * tube.NominalVoltage, todo)

		return WithStandardValues(cascadeDraft);
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