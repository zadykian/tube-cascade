using TubeCascade.Models;
using TubeCascade.Primitives;

namespace TubeCascade.Calculation;

/// <summary>
/// Input data for triode cascade calculation.
/// </summary>
/// <param name="InputSignal">
/// Supported input signal for cascade.
/// </param>
/// <param name="Tube">
/// Used vacuum triode.
/// </param>
/// <param name="NextCascadeInputResistance">
/// Input resistance of next cascade.
/// </param>
public readonly record struct CascadeInputData(
	InputSignal  InputSignal, 
	VacuumTriode Tube, 
	Resistance   NextCascadeInputResistance);