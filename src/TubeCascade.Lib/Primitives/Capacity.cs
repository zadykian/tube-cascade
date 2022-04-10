using TubeCascade.ElementList;

namespace TubeCascade.Primitives;

/// <summary>
/// Capacity in farads.
/// </summary>
public readonly record struct Capacity(double Value) : INominal
{
	/// <summary>
	/// Implicit cast operator '<see cref="Capacity"/> -> <see cref="double"/>'.
	/// </summary>
	public static implicit operator double(Capacity frequency) => frequency.Value;

	/// <summary>
	/// Implicit cast operator '<see cref="double"/> -> <see cref="Capacity"/>'.
	/// </summary>
	public static implicit operator Capacity(double value) => new(value);
}