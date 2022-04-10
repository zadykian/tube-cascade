using System.Collections.Immutable;
using System.Reflection;
using TubeCascade.ElementList;
using TubeCascade.Models;
using TubeCascade.Primitives;

using static TubeCascade.Calculation.Constants;

namespace TubeCascade.Calculation;

/// <inheritdoc />
public class CascadeCalculator : ICascadeCalculator
{
	private readonly IResistors resistors;
	private readonly ICapacitors capacitors;

	public CascadeCalculator(IResistors? resistors, ICapacitors? capacitors)
	{
		this.resistors  = resistors  ?? IResistors.Accurate;
		this.capacitors = capacitors ?? ICapacitors.Accurate;
	}

	private static readonly IReadOnlyCollection<PropertyInfo> cascadeElementProps =
		typeof(TriodeAmpCascade)
			.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.Where(prop => prop.DeclaringType!.IsAssignableTo(typeof(IElementBase)))
			.ToArray();

	/// <inheritdoc />
	public TriodeAmpCascade CalculateCascade(CascadeInputData inputData)
	{
		var cascadeDraft = new TriodeAmpCascade
		{
			Tube             = inputData.Tube,
			LeakResistor     = new Resistor(10 * Mega),
			SpuriousResistor = new Resistor(10 * Kilo),
			AnodeResistor    = new Resistor(02 * inputData.Tube.InternalResistance)
		};

		return WithStandardValues(cascadeDraft);
	}

	/// <summary>
	/// Set all nominal values of elements in <paramref name="cascadeDraft"/>
	/// to standard values based on data from <see cref="ICapacitors"/> and <see cref="IResistors"/>.
	/// </summary>
	private TriodeAmpCascade WithStandardValues(TriodeAmpCascade cascadeDraft)
	{
		IElementBase WithStandardNominal(IElementBase draftValue)
			=> draftValue switch
			{
				Capacitor capacitor => capacitors.NearestFromAbove(capacitor.Nominal),
				Resistor  resistor  => resistors.NearestFromAbove(resistor.Nominal),
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