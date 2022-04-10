using TubeCascade.ElementList;

namespace TubeCascade.Primitives;

/// <summary>
/// Resistance value on ohms.
/// </summary>
public readonly record struct Resistance(double Value) : INominal
{
	/// <summary>
	/// Implicit cast operator '<see cref="Resistance"/> -> <see cref="double"/>'.
	/// </summary>
	public static implicit operator double(Resistance voltage) => voltage.Value;

	/// <summary>
	/// Implicit cast operator '<see cref="double"/> -> <see cref="Resistance"/>'.
	/// </summary>
	public static implicit operator Resistance(double value) => new(value);
}