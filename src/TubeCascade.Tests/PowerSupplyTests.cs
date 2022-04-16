using NUnit.Framework;
using TubeCascade.Primitives;
using TubeCascade.Tests.Base;

namespace TubeCascade.Tests;

/// <summary>
/// Cascade calculator tests.
/// </summary>
public class PowerSupplyTests : CalculatorTestBase
{
	/// <summary>
	/// Calculate required nominal values of voltage and current
	/// for power supply for amplifying cascade based on vacuum triode.
	/// </summary>
	[Test]
	public void PowerSupplyForTubeCascade()
	{
		var calculator = Calculator();

		var inputVoltage = new Voltage(220);
		var cascade = calculator.CalculateCascade(InputWith6N1P());

		var powerSupply = calculator.CalculateRequiredPowerSupply(inputVoltage, cascade);
		Assert.AreEqual(inputVoltage, powerSupply.InputAcVoltage);
		Assert.IsNotEmpty(powerSupply.PowerUnits);
	}
}