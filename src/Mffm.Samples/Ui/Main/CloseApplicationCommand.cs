using Mffm.Contracts;
using Mffm.Samples.Properties;

namespace Mffm.Samples.Ui.Main;

internal class CloseApplicationCommand : IExtendedCommand
{
    private readonly IWindowManager _windowManager;

    public CloseApplicationCommand(IWindowManager windowManager)
    {
        _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
        Icon = Image.FromStream(new MemoryStream(Resources.icons_loeschen));
        ShortcutKeys = Keys.Control | Keys.Shift| Keys.Q;
    }
    public Image? Icon { get; }
    public Keys? ShortcutKeys { get; }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is not MainFormModel model) return;

        // we can pass the dialog result directly here
        _windowManager.Close(model, DialogResult.OK);
    }

    public event EventHandler? CanExecuteChanged;
    protected virtual void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}