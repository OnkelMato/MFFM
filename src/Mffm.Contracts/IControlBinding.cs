namespace Mffm.Contracts
{
    /// <summary>
    /// Interface for binding a control to a form model.
    /// </summary>
    public interface IControlBinding
    {
        /// <summary>
        /// Binds the specified control to the given form model.
        /// </summary>
        /// <param name="control">The control which should be bound to the form model.</param>
        /// <param name="formModel">The form model to bind to the control.</param>
        /// <returns>True if the control was handled; otherwise, false.</returns>
        bool Bind(Control control, IFormModel formModel);
    }
}