using System.ComponentModel;
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
                    // set value and attach to PropertyChanged event
                    label.Text = formModel.GetType().GetProperty(item.Name!)!.GetValue(formModel).ToString();

                    // ReSharper disable once SuspiciousTypeConversion.Global
                    if (formModel is INotifyPropertyChanged notifyModel)
                        notifyModel.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == item.Name)
                            {
                                label.Text = formModel.GetType().GetProperty(item.Name!)!.GetValue(formModel).ToString();
                            }
                        };
#endif
                }

            }

            return true;
        }
    }
}