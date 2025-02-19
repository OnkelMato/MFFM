using Mffm.Contracts;

namespace Mffm.Core.Bindings;

internal class ListBoxBinding : IControlBinding
{
    public bool Bind(Control control, IFormModel formModel)
    {
        if (control is not ListBox listBox) { return false; }

        if (formModel.GetType().GetProperty(listBox.Name) is not null)
            listBox.DataBindings.Add(
                new Binding(nameof(listBox.DataSource), formModel, listBox.Name, true, DataSourceUpdateMode.OnPropertyChanged));

        if (formModel.GetType().GetProperty(listBox.Name + "Selected") is not null)
            listBox.DataBindings.Add(
                new Binding(nameof(listBox.SelectedItem), formModel, listBox.Name + "Selected", true, DataSourceUpdateMode.OnPropertyChanged));
 
        listBox.SelectedIndexChanged += (sender, args) =>
        {
            formModel.GetType().GetProperty(control.Name + "Selected")?.SetValue(formModel, listBox.SelectedItem);
        };

        return true;
    }
}