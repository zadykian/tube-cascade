using System.Collections.Immutable;
using TubeCascade.ElementList;
using TubeCascade.Primitives;

namespace TubeCascade.Models;

/// <summary>
/// Tube's anode characteristic - dependency between anode currency and anode voltage
/// for specific voltage between tube's grid and anode.
/// </summary>
/// <param name="GridAnodeVoltage">
/// Voltage between tube's grid and anode.
/// </param>
/// <param name="VoltageToCurrentRatio">
/// <para>
/// Voltage (in volts) to current (in milliamperes) ratio.
/// </para>
/// <para>
/// Originally single ratio represented as a curve.
/// Linear dependency is used here for simplicity.
/// </para>
/// </param>
public readonly record struct AnodeCharacteristic(Voltage GridAnodeVoltage, double VoltageToCurrentRatio);

/// <summary>
/// Vacuum tube triode.
/// </summary>
/// <param name="NominalVoltage">
/// Tube's nominal voltage.
/// </param>
/// <param name="InternalResistance">
/// Tube's internal resistance.
/// </param>
/// <param name="AnodeCharacteristics">
/// Current-voltage characteristics of tube.
/// </param>
public readonly record struct VacuumTriode(
	Voltage                                  NominalVoltage,
	Resistance                               InternalResistance,
	IReadOnlyCollection<AnodeCharacteristic> AnodeCharacteristics)
{
	public VacuumTriode(
		Voltage nominalVoltage,
		Resistance internalResistance,
		IEnumerable<AnodeCharacteristic> anodeCharacteristics)
		: this(nominalVoltage, internalResistance, anodeCharacteristics.ToImmutableArray())
	{
	}
}