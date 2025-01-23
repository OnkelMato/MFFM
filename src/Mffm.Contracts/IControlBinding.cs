namespace Mffm.Contracts;

public interface IControlBinding
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="control">control which should be bound tot the formModel</param>
    /// <param name="formModel">FormModel to bind to the control</param>
    /// <returns>true if the control was handled</returns>
    bool Bind(Control control, IFormModel formModel);
}