using System.Windows.Input;
using Mffm.Contracts;

namespace Mffm.Core.Bindings;

internal class DefaultMenuItemBinding : IMenuItemBinding
{
    public void Bind(ToolStripMenuItem menuItem, IFormModel formModel)
    {
#if NET5_0_OR_GREATER
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
#else
        var command = formModel.GetType().GetProperty(menuItem.Name!)?.GetValue(formModel) as ICommand;
        if (command is null) return;

        menuItem.Click += (sender, args) => command.Execute(formModel);
        menuItem.Enabled = command.CanExecute(formModel);

        command.CanExecuteChanged += (sender, args) => menuItem.Enabled = command.CanExecute(formModel);


        // Bind the command in an old-fashioned way
        if (formModel.GetType().GetProperty(menuItem.Name!) is not null)
            menuItem.Click += (sender, args) =>
            {
                var property = formModel.GetType().GetProperty(menuItem.Name!);
                if (property != null)
                {
                    var command = property.GetValue(formModel) as ICommand;
                    command?.Execute(null);
                }
            };
      

        // Let's bind icons and shortcuts.
        // If the command is not IExtendCommand, we bind the icon and shortcut keys from the model by convention.
        if (formModel.GetType().GetProperty(menuItem.Name!)?.GetValue(formModel) is not IExtendedCommand extendedCommand)
        {
            if (formModel.GetType().GetProperty(menuItem.Name! + "Icon") is not null)
            {
                var propertyValue = formModel.GetType().GetProperty(menuItem.Name! + "Icon")?.GetValue(formModel) as Image;
                menuItem.Image = propertyValue;
            }

            if (formModel.GetType().GetProperty(menuItem.Name! + "ShortcutKeys") is not null)
            {
                var propertyValue = formModel.GetType().GetProperty(menuItem.Name! + "ShortcutKeys")?.GetValue(formModel);
                menuItem.ShortcutKeys = (Keys)propertyValue!;
            }
        }
        else
        {
            menuItem.Image = extendedCommand.Icon;
            menuItem.ShortcutKeys = extendedCommand.ShortcutKeys ?? Keys.None;
        }
#endif
    }
}