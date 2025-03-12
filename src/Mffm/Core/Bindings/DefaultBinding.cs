using Mffm.Contracts;

namespace Mffm.Core.Bindings;

internal class DefaultBinding : IControlBinding
{
    public bool Bind(Control control, IFormModel formModel)
    {
        if (formModel.GetType().GetProperty(control.Name) is null) return false;

        // it just binds to the text property
        if (control.DataBindings.HasNoBindingFor(nameof(control.Text)))
            control.DataBindings.Add(
                new Binding(nameof(control.Text), formModel, control.Name, true, DataSourceUpdateMode.OnPropertyChanged));

        return true;
    }
}