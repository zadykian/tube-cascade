namespace TubeCascade.Models;

/// <summary>
/// Signal amplifying cascade based on triode vacuum tube.
/// </summary>
public class TriodeAmpCascade
{
	/// <summary>
	/// Vacuum triode tube.
	/// </summary>
	public VacuumTriode Tube { get; init; }

	/// <summary>
	/// Input leak resistor which is required to prevent
	/// the accumulation of charge on the tube's control grid.
	/// </summary>
	/// <remarks>
	/// Marked as R1 on the circuit diagram.
	/// </remarks>
	public Resistor LeakResistor { get; init; }

	/// <summary>
	/// Input spurious resistor which is required to prevent
	/// high-frequency noise on the tube's grid and also prevents
	/// self-excitation of the cascade at high frequencies.
	/// </summary>
	/// <remarks>
	/// Marked as R2 on the circuit diagram.
	/// </remarks>
	public Resistor SpuriousResistor { get; init; }

	/// <summary>
	/// Tube's anode resistor which provides ability to set
	/// cascade's amplification degree.
	/// </summary>
	/// <remarks>
	/// Marked as RA on the circuit diagram.
	/// </remarks>
	public Resistor AnodeResistor { get; init; }

	/// <summary>
	/// Tube's cathode resistor which determines positive anode voltage bias.
	/// </summary>
	/// <remarks>
	/// Marked as RC on the circuit diagram.
	/// </remarks>
	public Resistor CathodeResistor { get; init; }

	/// <summary>
	/// Tube's cathode shunt capacitor.
	/// </summary>
	/// <remarks>
	/// Marked as CC on the circuit diagram.
	/// </remarks>
	public Capacitor CathodeCapacitor { get; init; }

	/// <summary>
	/// Cascade's isolation capacitor which is required to provide
	/// galvanic isolation between cascade's output and cascade's load.
	/// </summary>
	/// <remarks>
	/// Marked as IC on the circuit diagram.
	/// </remarks>
	public Capacitor IsolationCapacitor { get; init; }
}