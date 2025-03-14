using WinFormsMffmPrototype.MvvmFramework;

namespace WinFormsMffmPrototype.Ui.WithBindingSource;

internal class WithBindingSourceFormModel : IFormModel
{
    private IFormModel _formModel;

    public IFormModel FormModel => _formModel;

    public Image ButtonImage { get; set; }
}