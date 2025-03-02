namespace Mffm.Contracts;

/// <summary>
///     The binding manager is responsible for the data binding and connection between the FormModel and the Form
/// </summary>
public interface IBindingManager
{

    /// <summary>
    ///    Create the bindings between the FormModel and the Form
    /// </summary>
    /// <param name="formModel">FormModem to bind to form</param>
    /// <param name="form">Form to bind to formModel</param>
    void CreateBindings(IFormModel formModel, Form form);

    void CreateBindings(IFormModel formModel, Control control);
}