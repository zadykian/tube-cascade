using NUnit.Framework;
using TubeCascade.Tests.Base;

namespace TubeCascade.Tests;

/// <summary>
/// Cascade calculator tests.
/// </summary>
public class AmplifyingFunctionTests : CalculatorTestBase
{
	/// <summary>
	/// Get amplifying function for cascade based on 6N1P triode.
	/// </summary>
	[Test]
	public void GetAmplifyingFunctionTest()
	{
		var testInputData = InputWith6N1P();
		var calculator = Calculator();

		var cascade = calculator.CalculateCascade(testInputData);

		var amplifyingFunction = calculator.GetAmplifyingFunction(cascade);
		var (amplifiedVoltage, amplifiedRange) = amplifyingFunction(testInputData.InputSignal);

		Assert.AreEqual(amplifiedRange, testInputData.InputSignal.FrequencyRange);
		Assert.Greater(amplifiedVoltage, testInputData.InputSignal.VoltageAmplitude);
	}
}