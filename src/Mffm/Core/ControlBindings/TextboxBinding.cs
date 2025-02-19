using Mffm.Contracts;

namespace Mffm.Core.ControlBindings;

internal class TextboxBinding : IControlBinding
{
    // todo make this invariant!
    public bool Bind(Control control, IFormModel formModel)
    {
        if (control is not TextBox textBox) { return false; }

        textBox.DataBindings.Add(new Binding(nameof(textBox.Text), formModel, control.Name, true, DataSourceUpdateMode.OnPropertyChanged));

        // this is a hack so we change the value on change and not on leave. This might be important.
        //textBox.KeyPress += (sender, args) => { textBox?.DataBindings[nameof(textBox.Text)]?.WriteValue(); };

        return true;
    }
}