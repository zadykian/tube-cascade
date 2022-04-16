using TubeCascade.Models;
using TubeCascade.Primitives;

namespace TubeCascade.Calculation.Base;

/// <summary>
/// Signal amplifying cascade calculator. 
/// </summary>
public interface ICascadeCalculator<in TInputData, TCascade>
	where TInputData : ICascadeInputData
{
	/// <summary>
	/// Calculate nominal values for elements of amplifying cascade
	/// based on <paramref name="inputData"/>.
	/// </summary>
	/// <param name="inputData">
	/// Input data for cascade calculation.
	/// </param>
	TCascade CalculateCascade(TInputData inputData);

	/// <summary>
	/// Define a dependency between input signal and output for <paramref name="cascade"/>.
	/// </summary>
	/// <param name="cascade">
	/// Signal amplifying cascade.
	/// </param>
	Func<Signal, Signal> GetAmplifyingFunction(TCascade cascade);

	/// <summary>
	/// Determine required power supply for <paramref name="cascade"/> with <paramref name="inputAcVoltage"/>.
	/// </summary>
	/// <param name="inputAcVoltage">
	/// Input AC voltage.
	/// </param>
	/// <param name="cascade">
	/// Amplifying cascade.
	/// </param>
	PowerSupply CalculateRequiredPowerSupply(Voltage inputAcVoltage, TCascade cascade);
}