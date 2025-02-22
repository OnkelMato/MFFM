namespace Mffm.Contracts
{
    /// <summary>
    /// Represents a model for a form.
    /// </summary>
    public interface IFormModel
    {
        /// <summary>
        /// Gets or sets the context in case some data is needed.
        /// </summary>
        object? Context { set; }


    }
}