# tube-cascade
Библиотека для расчета элементной базы усилителя звука.

### Базовая функциональность
+ Определение номиналов для схемы усилителя на основе лампового триода с общим катодом.
+ Вычисление функции зависимости выходного сигнала от входного для рассчитанной элементной базы.
+ Приведение вычисленных значений сопротивления, емкости и напряжения к стандартным номинальным значениям (для резисторов и конденсаторов).
+ Расчет напряжения и мощности источника питания, необходимого для заданной элементной базы.

Библиотека содержит несколько базовых абстракций, позволяющих расширять и переопределять существующую логику

```c#
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
}
```

```c#
/// <summary>
/// Input data for cascade calculation.
/// </summary>
public interface ICascadeInputData
{
	/// <summary>
	/// Supported input signal for cascade.
	/// </summary>
	public Signal InputSignal { get; }

	/// <summary>
	/// Input resistance of next cascade.
	/// </summary>
	public Resistance NextCascadeInputResistance { get; }
}
```

### Класс `TubeCascadeCalculator` - Расчет каскада на ламповом триоде
Расчет каскада выполняется на основе входных параметров, представленных типом `TubeCascadeInputData`:
```c#
/// <summary>
/// Input data for triode cascade calculation.
/// </summary>
/// <param name="InputSignal">
/// Supported input signal for cascade.
/// </param>
/// <param name="Tube">
/// Used vacuum triode.
/// </param>
/// <param name="NextCascadeInputResistance">
/// Input resistance of next cascade.
/// </param>
public readonly record struct TubeCascadeInputData(
	Signal  InputSignal, 
	VacuumTriode Tube, 
	Resistance   NextCascadeInputResistance) : ICascadeInputData;
```

Входной сигнал определяется амплитудой по напряжению и частотным диапазоном.
Модель лампового триода включает в себя следующие характеристики:
+ `NominalVoltage` - номинальное напряжение триода
+ `InternalResistance` - внутреннее сопротивление триода
+ `AnodeCharacteristics` - семейство анодных характеристик лампы *

Таким образом класс `TubeCascadeCalculator` поддерживает вычисления для различных триодов с косвенным накалом. Описанные выше характеристики для конкретного триода могут быть получены из [базы данных электронных компонентов](https://rudatasheet.ru/tubes/)

\* В описании технических характеристик триодов анодные характеристики представлены в виде кривых. В данной библиотеки с целью упрощения API характеристики представлены линейными функциями.