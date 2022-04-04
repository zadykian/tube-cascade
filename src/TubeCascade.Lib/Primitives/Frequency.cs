namespace TubeCascade.Primitives;

/// <summary>
/// Frequency value.
/// </summary>
public readonly record struct Frequency(double Value) : IComparable<Frequency>
{
	/// <summary>
	/// Implicit cast operator '<see cref="Frequency"/> -> <see cref="double"/>'.
	/// </summary>
	public static implicit operator double(Frequency frequency) => frequency.Value;

	/// <summary>
	/// Implicit cast operator '<see cref="double"/> -> <see cref="Frequency"/>'.
	/// </summary>
	public static implicit operator Frequency(double value) => new(value);

	/// <inheritdoc />
	public int CompareTo(Frequency other) => Value.CompareTo(other.Value);
}