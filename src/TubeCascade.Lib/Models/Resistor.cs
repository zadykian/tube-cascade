using TubeCascade.Primitives;

namespace TubeCascade.Models;

/// <summary>
/// Resistor.
/// </summary>
/// <param name="Resistance">
/// Resistance in ohms.
/// </param>
public readonly record struct Resistor(Resistance Resistance);