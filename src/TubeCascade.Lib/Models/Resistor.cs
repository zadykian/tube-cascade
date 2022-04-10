using TubeCascade.ElementList;
using TubeCascade.Primitives;

namespace TubeCascade.Models;

/// <summary>
/// Resistor.
/// </summary>
/// <param name="Nominal">
/// Resistance in ohms.
/// </param>
public readonly record struct Resistor(Resistance Nominal) : IElement<Resistance>;