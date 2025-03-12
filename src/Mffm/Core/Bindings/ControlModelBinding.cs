using Mffm.Contracts;

namespace Mffm.Core.Bindings
{
    internal class ControlModelBinding : IControlBinding
    {
        private readonly Lazy<IBindingManager> _lazyBindingManager;

        public ControlModelBinding(Lazy<IBindingManager> bindingManager)
        {
            // problem: here is a circular dependency. One solution is deferred creation (e.g. singleton)
            _lazyBindingManager = bindingManager ?? throw new ArgumentNullException(nameof(bindingManager));
        }

        public bool Bind(Control control, Mffm.Contracts.IFormModel formModel)
        {
            var property = formModel.GetType().GetProperty(control.Name);
            if (property == null) return true; // as there is no prop, nothing will bind

            // let us see, of the property is a IFormModel.
            // this does not work for generic types, so there is a tagging interface without a generic parameter
            if (!typeof(IFormModel).IsAssignableFrom(property.PropertyType))
                return false; // it is not a IFormModel, so we do not bind it in this class

            var controlModel = property.GetValue(formModel);

            // how to make the dynamic data binding? maybe use the binding manager?
            var bindingManager = _lazyBindingManager.Value;
            bindingManager.CreateBindings(controlModel as IFormModel ?? throw new ArgumentException("The control model needs to be an IFormModel but it is not"), control);

            return true;
        }
    }
}