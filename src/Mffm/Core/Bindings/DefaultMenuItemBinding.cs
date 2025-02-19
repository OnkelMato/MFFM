using Mffm.Contracts;

namespace Mffm.Core.Bindings;

internal class DefaultMenuItemBinding : IMenuItemBinding
{
    public void Bind(ToolStripMenuItem menuItem, IFormModel formModel)
    {
        menuItem.DataBindings.Add(
            new Binding(nameof(menuItem.CommandParameter), formModel, null, true, DataSourceUpdateMode.Never));

        if (formModel.GetType().GetProperty(menuItem.Name!) is not null)
            menuItem.DataBindings.Add(
                new Binding(nameof(menuItem.Command), formModel, menuItem.Name, true, DataSourceUpdateMode.OnPropertyChanged));

        // Lets bind icons and shortcuts.
        // If the command is not IExtendCommand, we bind the icon and shortcut keys from the model by convention.
        if (formModel.GetType().GetProperty(menuItem.Name!)?.GetValue(formModel) is not IExtendedCommand command)
        {
            if (formModel.GetType().GetProperty(menuItem.Name! + "Icon") is not null)
                menuItem.DataBindings.Add(
                    new Binding(nameof(menuItem.Image), formModel, menuItem.Name + "Icon", true,
                        DataSourceUpdateMode.OnPropertyChanged));

            if (formModel.GetType().GetProperty(menuItem.Name! + "ShortcutKeys") is not null)
                menuItem.DataBindings.Add(
                    new Binding(nameof(menuItem.Image), formModel, menuItem.Name + "Icon", true,
                        DataSourceUpdateMode.OnPropertyChanged));
        }
        else
        {
            menuItem.Image = command.Icon;
            menuItem.ShortcutKeys = command.ShortcutKeys ?? Keys.None;
        }
    }
}