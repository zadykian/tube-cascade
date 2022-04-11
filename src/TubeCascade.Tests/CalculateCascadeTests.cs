using NUnit.Framework;
using TubeCascade.Primitives;
using static TubeCascade.Calculation.Constants;

// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace TubeCascade.Tests;

/// <summary>
/// Cascade calculator tests.
/// </summary>
public class CalculateCascadeTests : CalculatorTestBase
{
	/// <summary>
	/// Calculate cascade for 6N1P triode.
	/// </summary>
	[Test]
	public void CalculateFor6N1P()
	{
		var inputData = InputWith6N1P();
		var calculator = Calculator();
		var cascade = calculator.CalculateCascade(inputData);

		Assert.AreEqual(cascade.Tube, inputData.Tube);
		Assert.IsTrue(cascade.LeakResistor.Nominal.Between(new(100 * Kilo), new(10 * Mega)));
		Assert.IsTrue(cascade.SpuriousResistor.Nominal.Between(new(1 * Kilo), new(1 * Mega)));
		Assert.Greater(cascade.AnodeResistor.MaxVoltage, inputData.Tube.NominalVoltage);
		Assert.Greater(cascade.CathodeCapacitor.Nominal, new Capacity(5 * Micro));
		Assert.Greater(cascade.IsolationCapacitor.Nominal, new Capacity(5 * Micro));
	}
}