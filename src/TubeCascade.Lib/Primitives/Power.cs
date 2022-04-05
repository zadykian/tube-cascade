namespace TubeCascade.Primitives;

/// <summary>
/// Power value in watts.
/// </summary>
public readonly record struct Power(double Value)
{
	/// <summary>
	/// Implicit cast operator '<see cref="Power"/> -> <see cref="double"/>'.
	/// </summary>
	public static implicit operator double(Power voltage) => voltage.Value;

	/// <summary>
	/// Implicit cast operator '<see cref="double"/> -> <see cref="Power"/>'.
	/// </summary>
	public static implicit operator Power(double value) => new(value);
}