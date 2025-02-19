using Mffm.Contracts;

namespace Mffm.Core.ControlBindings;

internal class StatusStripBinding : IControlBinding
{
    // todo make this invariant!
    public bool Bind(Control control, IFormModel formModel)
    {
        if (control is not StatusStrip statusStrip) { return false; }

        foreach (ToolStripItem item in statusStrip.Items)
        {
            if (item is ToolStripStatusLabel label && formModel.GetType().GetProperty(item.Name) is not null)
            {
                label.DataBindings.Add(new Binding(nameof(label.Text), formModel, item.Name, true, DataSourceUpdateMode.OnPropertyChanged));
            }

        }

        return true;
    }
}