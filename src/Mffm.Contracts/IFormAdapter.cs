namespace Mffm.Contracts;

/// <summary>
/// Interface to attach a class to a form instead of the view model e.g. for menu extensions or other form related logic.
/// </summary>
public interface IFormAdapter
{
    /// <summary>
    /// Used for passing the form to the adapter. Double dispatch ;)
    /// </summary>
    /// <param name="form"></param>
    void InitializeWith(Form form);
}