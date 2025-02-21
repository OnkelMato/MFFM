namespace Mffm.Contracts
{
    /// <summary>
    ///     The windows manager is responsible for WinForms form management and creation.
    ///     This implementation knows about windows forms internal functions like Show() or ShowDialog()
    /// </summary>
    public interface IWindowManager
    {
        /// <summary>
        /// Shows a form of the specified type.
        /// </summary>
        /// <typeparam name="TFormModel">The type of the form model.</typeparam>
        void Show<TFormModel>() where TFormModel : class, IFormModel;

        /// <summary>
        /// Closes the specified form model.
        /// </summary>
        /// <param name="model">The form model to close.</param>
        void Close(IFormModel model);

        /// <summary>
        /// Runs a form of the specified type.
        /// </summary>
        /// <typeparam name="TFormModel">The type of the form model.</typeparam>
        void Run<TFormModel>() where TFormModel : class, IFormModel;

        /// <summary>
        /// Determines whether the specified form model is open.
        /// </summary>
        /// <param name="model">The form model to check.</param>
        /// <returns><c>true</c> if the form model is open; otherwise, <c>false</c>.</returns>
        bool IsFormOpen(IFormModel model);
    }
}