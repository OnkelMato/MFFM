namespace Mffm.Contracts;

/// <summary>
/// Supports binding of properties form the model to the form itself.
/// </summary>
public interface IFormBinding
{
    /// <summary>
    /// Supports binding of properties form the model to the form itself.
    /// Only a single binding per form is supported.
    /// </summary>
    /// <param name="form">For for binding</param>
    /// <param name="formModel">FormModel to bind to the control</param>
    void Bind(Form form, IFormModel formModel);
}