using TubeCascade.Primitives;

namespace TubeCascade.Models;

/// <summary>
/// Amplifying cascade input signal.
/// </summary>
/// <param name="VoltageAmplitude">
/// Input signal amplitude.
/// </param>
/// <param name="FrequencyRange">
/// Input signal range.
/// </param>
public readonly record struct InputSignal(Voltage VoltageAmplitude, Range<Frequency> FrequencyRange);