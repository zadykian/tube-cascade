using TubeCascade.Models;
using TubeCascade.Primitives;

namespace TubeCascade.Calculation;

/// <inheritdoc />
public class CascadeCalculator : ICascadeCalculator
{
	/// <inheritdoc />
	public TriodeAmpCascade CalculateCascade(CascadeInputData inputData)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public Func<InputSignal, Power> CalculatePower(TriodeAmpCascade triodeAmpCascade)
	{
		throw new NotImplementedException();
	}
}