namespace TubeCascade.ElementList;

/// <summary>
/// List of elements with standard nominal values.
/// </summary>
/// <typeparam name="TNominal">
/// Cascade circuit element's nominal type.
/// </typeparam>
/// <typeparam name="TElement">
/// Type of cascade circuit element.
/// </typeparam>
public interface IElementList<in TNominal, TElement>
	where TNominal : INominal
	where TElement : IElement<TNominal>, new()
{
	/// <summary>
	/// Get element which has nominal value nearest from above to <paramref name="nominal"/>.
	/// </summary>
	/// <param name="element"></param>
	TElement NearestFromAbove(TElement element);

	/// <summary>
	/// Get element which has nominal value nearest from below to <paramref name="nominal"/>.
	/// </summary>
	/// <param name="element"></param>
	TElement NearestFromBelow(TElement element);
}