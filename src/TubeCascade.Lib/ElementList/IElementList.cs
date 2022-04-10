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
public interface IElementList<in TNominal, out TElement>
	where TNominal : INominal
	where TElement : IElement<TNominal>
{
	/// <summary>
	/// 
	/// </summary>
	TElement? NearestFromAbove(TNominal nominal);

	TElement? NearestFromBelow(TNominal nominal);
}