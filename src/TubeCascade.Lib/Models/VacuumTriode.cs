using System.Collections.Immutable;
using TubeCascade.ElementList;
using TubeCascade.Primitives;

namespace TubeCascade.Models;

/// <summary>
/// Tube's anode characteristic - dependency between anode currency and anode voltage
/// for specific voltage between tube's grid and anode.
/// </summary>
public readonly struct AnodeCharacteristic
{
	public AnodeCharacteristic(Voltage gridAnodeVoltage)
	{
		GridAnodeVoltage = gridAnodeVoltage;
	}

	/// <summary>
	/// Voltage between tube's grid and anode.
	/// </summary>
	public Voltage GridAnodeVoltage { get; }
	
	// todo
}

/// <summary>
/// Vacuum tube triode.
/// </summary>
public readonly struct VacuumTriode
{
	public VacuumTriode(
		Voltage nominalVoltage,
		Resistance internalResistance,
		IEnumerable<AnodeCharacteristic> anodeCharacteristics)
	{
		NominalVoltage = nominalVoltage;
		InternalResistance = internalResistance;
		AnodeCharacteristics = anodeCharacteristics.ToImmutableArray();
	}

	/// <summary>
	/// Tube's nominal voltage.
	/// </summary>
	public Voltage NominalVoltage { get; }

	/// <summary>
	/// Tube's internal resistance.
	/// </summary>
	public Resistance InternalResistance { get; }

	/// <summary>
	/// Current-voltage characteristics of tube.
	/// </summary>
	public IReadOnlyCollection<AnodeCharacteristic> AnodeCharacteristics { get; }
}