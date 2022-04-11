using TubeCascade.Calculation.Base;
using TubeCascade.Models;
using TubeCascade.Primitives;

namespace TubeCascade.Calculation.Tube;

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
public readonly record struct TubeCascadeInputData(
	Signal  InputSignal, 
	VacuumTriode Tube, 
	Resistance   NextCascadeInputResistance) : ICascadeInputData;