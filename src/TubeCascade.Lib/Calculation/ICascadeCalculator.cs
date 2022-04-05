using TubeCascade.Models;
using TubeCascade.Primitives;

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
	TriodeAmpCascade CalculateCascade(CascadeInputData inputData);

	/// <summary>
	/// Define a dependency between input signal and output power for <paramref name="triodeAmpCascade"/>.
	/// </summary>
	/// <param name="triodeAmpCascade">
	/// Signal amplifying cascade based on triode vacuum tube.
	/// </param>
	Func<InputSignal, Power> CalculatePower(TriodeAmpCascade triodeAmpCascade);
}