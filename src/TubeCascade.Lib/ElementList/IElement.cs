namespace TubeCascade.ElementList;

/// <summary>
/// Cascade circuit element.
/// </summary>
public interface IElementBase
{
}

/// <inheritdoc cref="IElementBase"/>
/// <typeparam name="TNominal">
/// Type of element's value.
/// </typeparam>
public interface IElement<TNominal> : IElementBase
	where TNominal : INominal
{
	/// <summary>
	/// Element's value.
	/// </summary>
	TNominal Nominal { get; init; }
}