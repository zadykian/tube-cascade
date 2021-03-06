using NUnit.Framework;
using TubeCascade.Calculation.Tube;
using TubeCascade.ElementList;
using TubeCascade.Models;
using TubeCascade.Primitives;
using static TubeCascade.Calculation.Constants;
// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace TubeCascade.Tests.Base;

/// <summary>
/// Base test fixture.
/// </summary>
[TestFixture]
public abstract class CalculatorTestBase
{
	/// <summary>
	/// Create new instance implementing <see cref="ITubeCascadeCalculator"/>. 
	/// </summary>
	private protected static ITubeCascadeCalculator Calculator()
		=> new TubeCascadeCalculator(IResistors.Accurate, ICapacitors.Accurate);

	/// <summary>
	/// Create new <see cref="TubeCascadeInputData"/> value with 6N1P as triode. 
	/// </summary>
	private protected static TubeCascadeInputData InputWith6N1P()
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

		var inputSignal = new Signal(
			VoltageAmplitude: new Voltage(2),
			FrequencyRange:   (new Frequency(20), new Frequency(20 * Kilo)));

		return new TubeCascadeInputData(
			InputSignal:                inputSignal,
			Tube:                       tube,
			NextCascadeInputResistance: new Resistance(50 * Kilo));
	}
}