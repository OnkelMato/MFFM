namespace Mffm.Contracts;

/// <summary>
///     The windows manager is responsible for WinForms form management and creation.
///     This implementation knows about windows forms internal functions like Show() or ShowDialog()
/// </summary>
public interface IWindowManager
{
    void Show<TFormModel>() where TFormModel : class, IFormModel;
    void Close(IFormModel model);
    void Run<TFormModel>() where TFormModel : class, IFormModel;
    bool IsFormOpen(IFormModel model);
}