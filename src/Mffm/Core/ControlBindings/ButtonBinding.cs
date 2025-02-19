using Mffm.Contracts;

namespace Mffm.Core.ControlBindings;

internal class ButtonBinding : IControlBinding
{
    // todo make this invariant!
    public bool Bind(Control control, IFormModel formModel)
    {
        if (control is not Button button) { return false; }

        button.DataBindings.Add(new Binding(nameof(button.CommandParameter), formModel, null, true, DataSourceUpdateMode.Never));
        button.DataBindings.Add(new Binding(nameof(button.Command), formModel, control.Name, true, DataSourceUpdateMode.OnPropertyChanged));

        return true;
    }
}