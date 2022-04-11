namespace TubeCascade.Primitives;

/// <summary>
/// Current value. 
/// </summary>
public readonly record struct Current(double Value) : IComparable<Current>
{
	/// <summary>
	/// Implicit cast operator '<see cref="Current"/> -> <see cref="double"/>'.
	/// </summary>
	public static implicit operator double(Current voltage) => voltage.Value;

	/// <summary>
	/// Implicit cast operator '<see cref="double"/> -> <see cref="Current"/>'.
	/// </summary>
	public static implicit operator Current(double value) => new(value);

	/// <inheritdoc />
	public int CompareTo(Current other) => Value.CompareTo(other.Value);
}