using TubeCascade.ElementList;
using TubeCascade.Primitives;

namespace TubeCascade.Models;

/// <summary>
/// Capacitor.
/// </summary>
/// <param name="Nominal">
/// Capacity in farads.
/// </param>
/// <param name="MaxVoltage">
/// Maximum supported voltage.
/// </param>
public readonly record struct Capacitor(
	Capacity Nominal,
	Voltage  MaxVoltage) : IElement<Capacity>;