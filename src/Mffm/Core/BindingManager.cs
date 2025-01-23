using System.Diagnostics;
using System.Windows.Forms;
using Mffm.Contracts;

namespace Mffm.Core;

public class BindingManager(IEnumerable<IControlBinding> bindings) : IBindingManager
{
    // we need to reverse the bindings to have the most specific bindings first.
    // this ensures the default can 
    private readonly IEnumerable<IControlBinding> _bindings = bindings.Reverse() ?? [];

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
        var allItems = GetMenuItemsRecursively(form.MainMenuStrip?.Items);
        foreach (var menuItem in allItems)
        {
            if (string.IsNullOrEmpty(menuItem.Name)) continue;
            if (formModel.GetType().GetProperty(menuItem.Name) is null) continue;

            menuItem.DataBindings.Add(new Binding(nameof(menuItem.CommandParameter), formModel, null, true, DataSourceUpdateMode.Never));
            menuItem.DataBindings.Add(new Binding(nameof(menuItem.Command), formModel, menuItem.Name, true, DataSourceUpdateMode.OnPropertyChanged));


        }

        //foreach (var property in formModel.GetType().GetProperties())
        //{
        //    var menuItem = form.MainMenuStrip?.Items?.Find(property.Name, true)?.FirstOrDefault();
        //    if (menuItem is not null)
        //    {
        //        // we have to bind the commandparameter first to have it passed during the binding of the command itself
        //        continue;
        //    }
        //}
    }
}