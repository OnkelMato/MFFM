using System.Windows.Input;
using Mffm.Contracts;

namespace Mffm.Core.Bindings;

internal class ButtonBinding : IControlBinding
{
    // todo make this capital letters invariant!
    // todo add a "TryAddBindingGroup" that either adds all bindings or none (first is defining if dependent properties can be set)
    public bool Bind(Control control, IFormModel formModel)
    {
        if (control is not Button button) { return false; }

#if NET5_0_OR_GREATER
        // if command (main binding) is already set, do not bind again (none of both)
        if (!control.DataBindings.HasNoBindingFor(nameof(button.CommandParameter))) return false;

            button.DataBindings.Add(new Binding(nameof(button.CommandParameter), formModel, null, true, DataSourceUpdateMode.Never));
        if (control.DataBindings.HasNoBindingFor(nameof(button.Command)))
            button.DataBindings.Add(new Binding(nameof(button.Command), formModel, control.Name, true, DataSourceUpdateMode.OnPropertyChanged));
#else
        // Todo: Fix duplicate (recursive) bindings. Mayne this button already has a binding to the formModel.
        var command = formModel.GetType().GetProperty(button.Name!)?.GetValue(formModel) as ICommand;
        if (command is null) return false;

        button.Click += (sender, args) => command.Execute(formModel);
        button.Enabled = command.CanExecute(formModel);

        command.CanExecuteChanged += (sender, args) => button.Enabled = command.CanExecute(formModel);
#endif
        return true;
    }
}