using TubeCascade.Models;

namespace TubeCascade.Calculation;

/// <summary>
/// Vacuum triode amplifying cascade calculator.
/// </summary>
public interface ICascadeCalculator
{
	/// <summary>
	/// Calculate nominal values for elements of vacuum triode amplifying cascade
	/// based on <paramref name="inputData"/>.
	/// </summary>
	/// <param name="inputData">
	/// Input data for triode cascade calculation.
	/// </param>
	TriodeAmpCascade Calculate(CascadeInputData inputData);
}