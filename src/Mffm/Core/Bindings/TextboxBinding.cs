using Mffm.Contracts;
using System.Windows.Forms;

namespace Mffm.Core.Bindings;

internal class TextboxBinding : IControlBinding
{
    // todo make this invariant!
    public bool Bind(Control control, IFormModel formModel)
    {
        if (formModel.GetType().GetProperty(control.Name) is null) return false;
        if (control is not TextBox textBox) return false;

        // check if binding already exists
        if (control.DataBindings.HasNoBindingFor(nameof(textBox.Text)))
            textBox.DataBindings.Add(
                new Binding(nameof(textBox.Text), formModel, control.Name, true, DataSourceUpdateMode.OnPropertyChanged));

        return true;
    }
}