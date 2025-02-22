using Mffm.Contracts;

namespace Mffm.Core.ControlBindings
{
    internal class DefaultBinding : IControlBinding
    {
        public bool Bind(Control control, IFormModel formModel)
        {
            if (formModel.GetType().GetProperty(control.Name) is null) return false;
        
            // it just binds to the text property
            control.DataBindings.Add(
                new Binding(nameof(control.Text), formModel, control.Name, true, DataSourceUpdateMode.OnPropertyChanged));

            return true;
        }
    }
}