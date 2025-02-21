using Mffm.Contracts;

namespace Mffm.Core.Bindings;

internal class ButtonBinding : IControlBinding
{
    // todo make this invariant!
    public bool Bind(Control control, IFormModel formModel)
    {
        if (control is not Button button) { return false; }

        button.DataBindings.Add(
            new Binding(nameof(button.CommandParameter), formModel, null, true, DataSourceUpdateMode.Never));

        if (formModel.GetType().GetProperty(button.Name) is not null)
            button.DataBindings.Add(
                new Binding(nameof(button.Command), formModel, button.Name, true, DataSourceUpdateMode.OnPropertyChanged));

        return true;
    }
}