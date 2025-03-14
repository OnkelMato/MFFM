using System.ComponentModel;

namespace WinFormsMffmPrototype.Ui.WithBindingSource;

public class ExtendedButton : Button
{
    [Bindable(true)]
#pragma warning disable WFO1000
    public Image? ImageBinding
#pragma warning restore WFO1000
    {
        get => base.Image;
        set => base.Image = value;
    }
}