using Mffm.Contracts;

namespace Mffm.Core.ControlBindings
{
    internal class ButtonBinding : IControlBinding
    {
        // todo make this invariant!
        public bool Bind(Control control, IFormModel formModel)
        {
            if (control is not Button button) { return false; }

#if NET5_0_OR_GREATER
            button.DataBindings.Add(new Binding(nameof(button.CommandParameter), formModel, null, true, DataSourceUpdateMode.Never));
            button.DataBindings.Add(new Binding(nameof(button.Command), formModel, control.Name, true, DataSourceUpdateMode.OnPropertyChanged));
#else
            // todo fixme, but how? button decorator? button binding adapter?
#endif
            return true;
        }
    }
}