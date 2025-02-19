using Mffm.Contracts;

namespace Mffm.Core.Bindings;

internal class TextboxBinding : IControlBinding
{
    // todo make this invariant!
    public bool Bind(Control control, IFormModel formModel)
    {
        if (control is not TextBox textBox) { return false; }

        if (formModel.GetType().GetProperty(textBox.Name) is not null)
            textBox.DataBindings.Add(
                new Binding(nameof(textBox.Text), formModel, textBox.Name, true, DataSourceUpdateMode.OnPropertyChanged));

        return true;
    }
}