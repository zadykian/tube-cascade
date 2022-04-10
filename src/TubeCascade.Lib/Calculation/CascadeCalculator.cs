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
			LeakResistor     = new Resistor(10 * Mega),
			SpuriousResistor = new Resistor(10 * Kilo),
			AnodeResistor    = new Resistor(02 * inputData.Tube.InternalResistance)
		};

		var elementsWithNominalValues = cascadeElementProps
			.Select(prop => (IElementBase)prop.GetValue(cascadeDraft)!)
			.Select(element => element switch
			{
				Capacitor capacitor => (IElementBase) capacitors.NearestFromAbove(capacitor.Nominal),
				Resistor  resistor  => resistors.NearestFromAbove(resistor.Nominal),
				_ => throw new NotSupportedException($"Element with type '{element.GetType()}' is not supported.")
			})
			.ToImmutableArray();

		var cascadeWithNominalElements = new TriodeAmpCascade();

		return cascadeWithNominalElements;
	}

	/// <inheritdoc />
	public Func<InputSignal, Power> CalculatePower(TriodeAmpCascade triodeAmpCascade)
	{
		throw new NotImplementedException();
	}
}