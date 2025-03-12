using Mffm.Contracts;

namespace Mffm.Core.Bindings;

internal class TextboxBinding : IControlBinding
{
    // todo make this invariant!
    public bool Bind(Control control, IFormModel formModel)
    {
        if (formModel.TryFindProperty(control.Name)) return false;
        if (formModel.GetType().GetProperty(control.Name) is null) return false;
        if (control is not TextBox) return false;

        control.DataBindings.TryAddBinding(nameof(TextBox.Text), formModel, control.Name);

        return true;
    }
}