using TubeCascade.Primitives;

namespace TubeCascade.Models;

/// <summary>
/// Audio signal.
/// </summary>
/// <param name="VoltageAmplitude">
/// Signal amplitude.
/// </param>
/// <param name="FrequencyRange">
/// Signal range.
/// </param>
public readonly record struct Signal(Voltage VoltageAmplitude, Range<Frequency> FrequencyRange);