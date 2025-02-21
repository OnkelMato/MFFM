namespace Mffm.Contracts;

/// <summary>
/// Supports binding of properties form the model to the menu items. One binding per form is supported.
/// </summary>
public interface IMenuItemBinding
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item">item which should be bound to the formModel</param>
    /// <param name="formModel">FormModel to bind to the control</param>
    void Bind(ToolStripMenuItem item, IFormModel formModel);
}