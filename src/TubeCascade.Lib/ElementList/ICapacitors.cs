using TubeCascade.Models;
using TubeCascade.Primitives;

namespace TubeCascade.ElementList;

/// <summary>
/// List of capacitors with standard nominal values of capacity.
/// </summary>
public interface ICapacitors : IElementList<Capacity, Capacitor>
{
	/// <summary>
	/// Default capacitors list which does not any roundings to nominal values.
	/// </summary>
	/// <remarks>
	/// Should be used when either accurate values of <see cref="Capacity"/> are requires
	/// or nominal values of physical elements are unknown. 
	/// </remarks>
	public static ICapacitors Accurate => new DefaultCapacitors();

	/// <inheritdoc />
	private sealed class DefaultCapacitors : ICapacitors
	{
		/// <inheritdoc />
		Capacitor IElementList<Capacity, Capacitor>.NearestFromAbove(Capacity nominal)
			=> new(nominal);

		/// <inheritdoc />
		Capacitor IElementList<Capacity, Capacitor>.NearestFromBelow(Capacity nominal)
			=> new(nominal);
	}
}