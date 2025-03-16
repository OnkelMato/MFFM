namespace Mffm.Contracts;

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
    void Show<TFormModel>(object? context = null) where TFormModel : class, IFormModel;

    /// <summary>
    /// Shows a form of the specified type.
    /// </summary>
    /// <typeparam name="TFormModel">The type of the form model.</typeparam>
    DialogResult ShowModal<TFormModel>(object? context = null) where TFormModel : class, IFormModel;

    /// <summary>
    /// Closes the specified form model.
    /// </summary>
    /// <param name="model">The form model to close.</param>
    /// <param name="dialogResult">Overrides the form model DialogResult property for the result.</param>
    bool Close(IFormModel model, DialogResult? dialogResult = null);

    /// <summary>
    /// Runs a form of the specified type.
    /// </summary>
    /// <typeparam name="TFormModel">The type of the form model.</typeparam>
    DialogResult Run<TFormModel>(object? context = null) where TFormModel : class, IFormModel;

    /// <summary>
    /// Determines whether the specified form model is open.
    /// </summary>
    /// <param name="model">The form model to check.</param>
    /// <returns><c>true</c> if the form model is open; otherwise, <c>false</c>.</returns>
    bool IsFormOpen(IFormModel model);
}