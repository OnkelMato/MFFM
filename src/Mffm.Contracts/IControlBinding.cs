namespace Mffm.Contracts;

/// <summary>
/// Supports binding of properties form the model to the controls. Multiple bindings per form are supported.
/// </summary>
public interface IControlBinding
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="control">control which should be bound to the formModel</param>
    /// <param name="formModel">FormModel to bind to the control</param>
    /// <returns>true if the control was handled</returns>
    bool Bind(Control control, IFormModel formModel);
}