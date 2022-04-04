namespace TubeCascade.Primitives;

/// <summary>
/// Voltage value. 
/// </summary>
public readonly record struct Voltage(double Value)
{
	/// <summary>
	/// Implicit cast operator '<see cref="Voltage"/> -> <see cref="double"/>'.
	/// </summary>
	public static implicit operator double(Voltage voltage) => voltage.Value;

	/// <summary>
	/// Implicit cast operator '<see cref="double"/> -> <see cref="Voltage"/>'.
	/// </summary>
	public static implicit operator Voltage(double value) => new(value);
}