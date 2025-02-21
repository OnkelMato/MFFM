using System.Diagnostics;
using Mffm.Contracts;

namespace Mffm.Core;

internal class BindingManager : IBindingManager
{
    private readonly IFormBinding _formBinding;
    private readonly IMenuItemBinding _menuItemBinding;

    // we need to reverse the bindings to have the most specific bindings first.
    // this ensures the default can 
    private readonly IEnumerable<IControlBinding> _bindings;

    public BindingManager(IEnumerable<IControlBinding> bindings, IFormBinding formBinding, IMenuItemBinding menuItemBinding)
    {
        _formBinding = formBinding ?? throw new ArgumentNullException(nameof(formBinding));
        _menuItemBinding = menuItemBinding ?? throw new ArgumentNullException(nameof(menuItemBinding));
        _bindings = bindings.Reverse();
    }

    #region GetAllControls helper
    private IEnumerable<Control> GetControlsRecursively(Control control)
    {
        if (control.Controls.Count == 0) { return []; }

        var result = new List<Control>();
        foreach (Control innerControl in control.Controls)
        {
            result.Add(innerControl);
            result.AddRange(GetControlsRecursively(innerControl));
        }

        return result;
    }

    private IEnumerable<Control> GetAllControls(Form form)
    {
        var result = new List<Control>();

        foreach (Control control in form.Controls)
        {
            result.Add(control);
            result.AddRange(GetControlsRecursively(control));
        }

        return result;
    }
    #endregion

    #region GetAllMenuItems helper

    private IEnumerable<ToolStripMenuItem> GetMenuItemsRecursively(ToolStripItemCollection? items)
    {
        if (items is null) { return []; }

        var result = new List<ToolStripMenuItem>();
        foreach (var item in items)
        {
            if (item is ToolStripMenuItem menuItem)
            {
                result.Add(menuItem);
                result.AddRange(GetMenuItemsRecursively(menuItem.DropDownItems));
            }
        }
        return result;
    }

    #endregion

    public void CreateBindings(IFormModel formModel, Form form)
    {
        // let us iterate over all controls and let the binding handle the control to model binding
        foreach (Control formControl in GetAllControls(form))
        {
            Debug.WriteLine($"Control {formControl.Name} found of type {formControl.GetType().Name}");
            foreach (var controlBinding in _bindings)
            {
                // binding can return if the binding was handles
                if (controlBinding.Bind(formControl, formModel))
                {
                    Debug.WriteLine($"Control {formControl.Name} was handled by {controlBinding.GetType().Name}");
                    break; // because it was handled
                }
            }
        }

        // the menu strip is a special case but not worth create a separate extensibility (like above with controls)
        // refactor this to IBinding so it can be extended
        var allItems = GetMenuItemsRecursively(form.MainMenuStrip?.Items);
        foreach (var menuItem in allItems)
        {
            if (string.IsNullOrEmpty(menuItem.Name)) continue;
            _menuItemBinding.Bind(menuItem, formModel);
        }

        // lets bind the form itself
        _formBinding.Bind(form, formModel);
    }
}