﻿using System;
using System.Windows.Forms;
using Mffm.Contracts;

namespace LinkManager48.MffmExtensions
{
    internal class ControlModelBinding : IControlBinding
    {
        private readonly Lazy<IBindingManager> _lazyBindingManager;

        // problem: here is a circular dependency. One solution is deferred creation (e.g. singleton)
        public ControlModelBinding(Lazy<IBindingManager> bindingManager)
        {
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
            bindingManager.CreateBindings(controlModel as IFormModel, control);

            return true;
        }
    }
}