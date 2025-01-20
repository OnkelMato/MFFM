using Mffm.Contracts;

namespace Mffm.Core;

/// <summary>
///     The windows manager is responsible for WinForms form management and creation. It
///     knows about windows forms internal functions like Show() or ShowDialog().
///     The dictionary _openWindows is used to keep track of the open windows. An event
///     handler is added to the FormClosed event of the form to remove the form from the
///     dictionary.
/// </summary>
/// <param name="bindingManager"></param>
public class WindowManager(IBindingManager bindingManager) : IWindowManager
{
    private readonly IBindingManager _bindingManager =
        bindingManager ?? throw new ArgumentNullException(nameof(bindingManager));

    private readonly Dictionary<IFormModel, WeakReference<Form>> _openWindows = new();

    public void Show<TFormModel>() where TFormModel : class, IFormModel
    {
        var form = _bindingManager.GetFormFor(out TFormModel model);

        _openWindows.Add(model, new WeakReference<Form>(form));
        form.FormClosed += (sender, args) => _openWindows.Remove(model);
        form!.Show();
    }

    public void Close(IFormModel model)
    {
        var hasWindow = _openWindows[model].TryGetTarget(out var form);
        if (!hasWindow) return;

        form!.Close();
        _openWindows.Remove(model);
    }

    public void Run<TFormModel>() where TFormModel : class, IFormModel
    {
        var form = _bindingManager.GetFormFor<TFormModel>(out var model)
                   ?? throw new Exception($"Cannot find the form for ${typeof(TFormModel)}");

        _openWindows.Add(model, new WeakReference<Form>(form));
        form.FormClosed += (sender, args) => _openWindows.Remove(model);

        Application.Run(form);
    }

    public bool IsFormOpen(IFormModel model)
    {
        return _openWindows.ContainsKey(model);
    }
}