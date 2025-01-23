using Mffm.Contracts;

namespace Mffm.Core.ControlBindings;

public class ListBoxBinding : IControlBinding
{
    public bool Bind(Control control, IFormModel formModel)
    {
        if (control is not ListBox listBox) { return false; }

        listBox.DataBindings.Add(new Binding(nameof(listBox.DataSource), formModel, control.Name, true, DataSourceUpdateMode.OnPropertyChanged));
        listBox.DataBindings.Add(new Binding(nameof(listBox.SelectedItem), formModel, control.Name + "Selected", true, DataSourceUpdateMode.OnPropertyChanged));
        listBox.SelectedIndexChanged += (sender, args) =>
        {
            formModel.GetType().GetProperty(control.Name + "Selected")?.SetValue(formModel, listBox.SelectedItem);
        };

        return true;
    }
}