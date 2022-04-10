using TubeCascade.ElementList;
using TubeCascade.Primitives;

namespace TubeCascade.Models;

/// <summary>
/// Capacitor.
/// </summary>
/// <param name="Nominal">
/// Capacitor in farads.
/// </param>
public readonly record struct Capacitor(Capacity Nominal) : IElement<Capacity>;