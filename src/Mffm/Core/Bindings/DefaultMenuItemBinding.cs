using Mffm.Contracts;

namespace Mffm.Core.Bindings;

internal class DefaultMenuItemBinding : IMenuItemBinding
{
    public void Bind(ToolStripItem menuItem, IFormModel formModel)
    {
        menuItem.DataBindings.Add(
            new Binding(nameof(menuItem.CommandParameter), formModel, null, true, DataSourceUpdateMode.Never));

        if (formModel.GetType().GetProperty(menuItem.Name!) is not null)
            menuItem.DataBindings.Add(
                new Binding(nameof(menuItem.Command), formModel, menuItem.Name, true, DataSourceUpdateMode.OnPropertyChanged));

        if (formModel.GetType().GetProperty(menuItem.Name! + "Icon") is not null)
            menuItem.DataBindings.Add(
                new Binding(nameof(menuItem.Image), formModel, menuItem.Name + "Icon", true, DataSourceUpdateMode.OnPropertyChanged));
    }
}