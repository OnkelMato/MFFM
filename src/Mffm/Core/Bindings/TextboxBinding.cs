using Mffm.Contracts;

namespace Mffm.Core.Bindings;

internal class TextboxBinding : IControlBinding
{
    // todo make this invariant!
    public bool Bind(Control control, IFormModel formModel)
    {
        if (formModel.GetType().GetProperty(control.Name) is null) return false;
        if (control is not TextBox textBox) return false;

        textBox.DataBindings.Add(new Binding(nameof(textBox.Text), formModel, control.Name, true, DataSourceUpdateMode.OnPropertyChanged));

        return true;
    }
}