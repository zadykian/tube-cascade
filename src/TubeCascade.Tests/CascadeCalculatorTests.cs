using NUnit.Framework;
using TubeCascade.Calculation;
using TubeCascade.ElementList;
using TubeCascade.Models;
using TubeCascade.Primitives;

using static TubeCascade.Calculation.Constants;
// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace TubeCascade.Tests;

/// <summary>
/// Cascade calculator tests.
/// </summary>
[TestFixture]
public class CascadeCalculatorTests
{
	/// <summary>
	/// Create new instance implementing <see cref="ICascadeCalculator"/>. 
	/// </summary>
	private static ICascadeCalculator Calculator()
		=> new CascadeCalculator(IResistors.Accurate, ICapacitors.Accurate);

	/// <summary>
	/// Calculate cascade for 6N1P triode.
	/// </summary>
	[Test]
	public void CalculateFor6N1P()
	{
		var tubeAnodeCharacteristics = new AnodeCharacteristic[]
		{
			new(GridAnodeVoltage: new(014), VoltageToCurrentRatio: 00.29),
			new(GridAnodeVoltage: new(012), VoltageToCurrentRatio: 00.53),
			new(GridAnodeVoltage: new(010), VoltageToCurrentRatio: 00.62),
			new(GridAnodeVoltage: new(008), VoltageToCurrentRatio: 00.80),
			new(GridAnodeVoltage: new(006), VoltageToCurrentRatio: 01.14),
			new(GridAnodeVoltage: new(004), VoltageToCurrentRatio: 02.67),
			new(GridAnodeVoltage: new(002), VoltageToCurrentRatio: 04.00),
			new(GridAnodeVoltage: new(000), VoltageToCurrentRatio: 06.40),
			new(GridAnodeVoltage: new(-02), VoltageToCurrentRatio: 12.00),
			new(GridAnodeVoltage: new(-04), VoltageToCurrentRatio: 22.00),
			new(GridAnodeVoltage: new(-06), VoltageToCurrentRatio: 52.00),
			new(GridAnodeVoltage: new(-08), VoltageToCurrentRatio: 68.00),
			new(GridAnodeVoltage: new(-10), VoltageToCurrentRatio: 72.00),
		};

		var tube = new VacuumTriode(
			NominalVoltage:       new Voltage(250),
			InternalResistance:   new Resistance(11 * Kilo),
			AnodeCharacteristics: tubeAnodeCharacteristics);

		var inputSignal = new InputSignal(
			VoltageAmplitude: new Voltage(2),
			FrequencyRange:   (new Frequency(20), new Frequency(20 * Kilo)));

		var inputData = new CascadeInputData(
			InputSignal:                inputSignal,
			Tube:                       tube,
			NextCascadeInputResistance: new Resistance(50 * Kilo));

		var calculator = Calculator();
		var cascade = calculator.CalculateCascade(inputData);

		Assert.AreEqual(cascade.Tube, inputData.Tube);
		// todo
	}
}