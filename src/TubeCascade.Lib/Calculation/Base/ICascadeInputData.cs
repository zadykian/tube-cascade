using TubeCascade.Models;
using TubeCascade.Primitives;

namespace TubeCascade.Calculation.Base;

/// <summary>
/// Input data for cascade calculation.
/// </summary>
public interface ICascadeInputData
{
	/// <summary>
	/// Supported input signal for cascade.
	/// </summary>
	public Signal InputSignal { get; }

	/// <summary>
	/// Input resistance of next cascade.
	/// </summary>
	public Resistance NextCascadeInputResistance { get; }
}