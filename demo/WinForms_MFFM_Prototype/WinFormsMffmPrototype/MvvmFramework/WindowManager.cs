namespace WinFormsMffmPrototype.MvvmFramework;

public class WindowManager : IWindowManager
{
    private readonly IServiceProvider _services;
    private readonly Dictionary<object, WeakReference<Form?>> _openWindows = new();

    public WindowManager(IServiceProvider services)
    {
        _services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public void Show<TForm>() where TForm : Form, IWindow
    {
        var form = (TForm)_services.GetService(typeof(TForm));
        var model = (form as IWindow).FormModel;
        _openWindows.Add(model, new WeakReference<Form>(form));
        form.FormClosed += (sender, args) => _openWindows.Remove(model);
        form!.Show();
    }

    public bool IsFormOpen(object parameter)
    {
        return _openWindows.ContainsKey(parameter);
    }

    public void CloseForm(object parameter)
    {
        var hasWindow = _openWindows[parameter].TryGetTarget(out Form? form);
        if (!hasWindow) return;
        form.Close();
        _openWindows.Remove(parameter);
    }
}