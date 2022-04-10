namespace TubeCascade.ElementList;

/// <summary>
/// Cascade circuit element.
/// </summary>
/// <typeparam name="TNominal">
/// Type of element's nominal.
/// </typeparam>
public interface IElement<out TNominal> where TNominal : INominal
{
	/// <summary>
	/// Element's nominal value.
	/// </summary>
	TNominal Nominal { get; }
}