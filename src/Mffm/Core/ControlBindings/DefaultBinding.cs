using Mffm.Contracts;

namespace Mffm.Core.ControlBindings;

public class DefaultBinding : IControlBinding
{
    public bool Bind(Control control, IFormModel formModel)
    {
        if (formModel.GetType().GetProperty(control.Name) is null) return false;
        
        //return false;
        // it just binds to the text property
        var binding = new Binding(nameof(control.Text), formModel, control.Name, true, DataSourceUpdateMode.OnPropertyChanged);
        control.DataBindings.Add(binding);

        return true;
    }
}