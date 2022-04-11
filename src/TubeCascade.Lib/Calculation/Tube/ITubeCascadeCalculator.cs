using TubeCascade.Calculation.Base;
using TubeCascade.Models;

namespace TubeCascade.Calculation.Tube;

/// <summary>
/// Vacuum triode amplifying cascade calculator.
/// </summary>
public interface ITubeCascadeCalculator : ICascadeCalculator<TubeCascadeInputData, TriodeAmpCascade>
{
}