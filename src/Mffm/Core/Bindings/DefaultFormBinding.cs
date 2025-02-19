using Mffm.Contracts;

namespace Mffm.Core.Bindings;

internal class DefaultFormBinding : IFormBinding
{
    public void Bind(Form form, IFormModel formModel)
    {
        if (formModel.GetType().GetProperty("Title") is not null)
        {
            form.DataBindings.Add(new Binding(nameof(form.Text), formModel, "Title", true, DataSourceUpdateMode.Never));
        }
    }
}