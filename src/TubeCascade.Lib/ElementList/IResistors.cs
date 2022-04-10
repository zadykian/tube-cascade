using TubeCascade.Models;
using TubeCascade.Primitives;

namespace TubeCascade.ElementList;

/// <summary>
/// List of resistors with nominal values of capacity.
/// </summary>
public interface IResistors : IElementList<Resistance, Resistor>
{
	/// <summary>
	/// Default capacitors list which does not any roundings to nominal values.
	/// </summary>
	/// <remarks>
	/// Should be used when either accurate values of <see cref="Capacity"/> are requires
	/// or nominal values of physical elements are unknown. 
	/// </remarks>
	public static IResistors Accurate => new DefaultIResistors();

	/// <inheritdoc />
	private sealed class DefaultIResistors : IResistors
	{
		/// <inheritdoc />
		Resistor IElementList<Resistance, Resistor>.NearestFromAbove(Resistance nominal)
			=> new(nominal);

		/// <inheritdoc />
		Resistor IElementList<Resistance, Resistor>.NearestFromBelow(Resistance nominal)
			=> new(nominal);
	}
}