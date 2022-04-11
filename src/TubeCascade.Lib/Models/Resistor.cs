using TubeCascade.ElementList;
using TubeCascade.Primitives;

namespace TubeCascade.Models;

/// <summary>
/// Resistor.
/// </summary>
/// <param name="Nominal">
/// Resistance in ohms.
/// </param>
/// <param name="MaxVoltage">
/// Maximum supported voltage.
/// </param>
/// <param name="MaxDissipatedPower">
/// Maximum supported dissipated power.
/// </param>
public readonly record struct Resistor(
	Resistance Nominal,
	Voltage    MaxVoltage,
	Power      MaxDissipatedPower) : IElement<Resistance>;