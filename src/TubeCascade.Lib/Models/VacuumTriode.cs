using TubeCascade.ElementList;
using TubeCascade.Primitives;

namespace TubeCascade.Models;

/// <summary>
/// Vacuum tube triode.
/// </summary>
public readonly struct VacuumTriode
{
	public VacuumTriode(Voltage nominalVoltage, Resistance internalResistance)
	{
		NominalVoltage = nominalVoltage;
		InternalResistance = internalResistance;
	}

	/// <summary>
	/// Tube's nominal voltage.
	/// </summary>
	public Voltage NominalVoltage { get; }

	/// <summary>
	/// Tube's internal resistance.
	/// </summary>
	public Resistance InternalResistance { get; }
}