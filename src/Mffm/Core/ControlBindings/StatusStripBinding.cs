using Mffm.Contracts;

namespace Mffm.Core.ControlBindings
{
    internal class StatusStripBinding : IControlBinding
    {
        // todo make this invariant!
        public bool Bind(Control control, IFormModel formModel)
        {
            if (control is not StatusStrip statusStrip) { return false; }

            foreach (ToolStripItem item in statusStrip.Items)
            {
                if (string.IsNullOrEmpty(item.Name)) continue;

                if (item is ToolStripStatusLabel label && formModel.GetType().GetProperty(item.Name) is not null)
                {
#if NET5_0_OR_GREATER
                    label.DataBindings.Add(new Binding(nameof(label.Text), formModel, item.Name, true, DataSourceUpdateMode.OnPropertyChanged));
#else
                    // todo fixme, but how? button decorator? button binding adapter?
#endif
                }

            }

            return true;
        }
    }
}