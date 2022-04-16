using TubeCascade.Primitives;

namespace TubeCascade.Models;

/// <summary>
/// Single power unit supplying direct current.
/// </summary>
/// <param name="Voltage">
/// Unit's output voltage.
/// </param>
/// <param name="Current">
/// Unit's output current.
/// </param>
public readonly record struct DcPowerUnit(Voltage Voltage, Current Current);

/// <summary>
/// Power supply for amplifying cascade.
/// </summary>
/// <param name="InputAcVoltage">
/// Required input alternating current.
/// </param>
/// <param name="PowerUnits">
/// List of provided output voltage-current units.
/// </param>
public readonly record struct PowerSupply(
	Voltage InputAcVoltage,
	IReadOnlyCollection<DcPowerUnit> PowerUnits);