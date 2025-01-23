using System.Diagnostics;
using Mffm.Contracts;

namespace Mffm.Core;

/// <summary>
///     The windows manager is responsible for WinForms form management and creation. It
///     knows about windows forms internal functions like Show() or ShowDialog().
///     The dictionary _openWindows is used to keep track of the open windows. An event
///     handler is added to the FormClosed event of the form to remove the form from the
///     dictionary.
/// </summary>
/// <param name="bindingManager"></param>
public class WindowManager(IServiceProvider serviceProvider, IBindingManager bindingManager, IFormMapper formMapper) : IWindowManager
{
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    private readonly IBindingManager _bindingManager = bindingManager ?? throw new ArgumentNullException(nameof(bindingManager));
    private readonly IFormMapper _formMapper = formMapper ?? throw new ArgumentNullException(nameof(formMapper));

    // keep track of all open windows so we can close them
    private readonly Dictionary<IFormModel, WeakReference<Form>> _openWindows = new();

    private Form GetFormFor<TFormModel>(out TFormModel formModel)
        where TFormModel : class, IFormModel
    {
        formModel = _serviceProvider.GetService(typeof(TFormModel)) as TFormModel ?? throw new ServiceNotFoundException($"Cannot fond service for ${typeof(TFormModel).Name}");

        var formType = _formMapper.GetFormFor<TFormModel>();
        var form = _serviceProvider.GetService(formType) as Form;
        if (form is null) throw new ServiceNotFoundException($"Cannot fond service for ${formType.Name}");

        // binding manager is responsible for the data binding and connection between the FormModel and the Form
        _bindingManager.CreateBindings(formModel, form);

        return form;
    }

    public void Show<TFormModel>() where TFormModel : class, IFormModel
    {
        var form = GetFormFor(out TFormModel model);

        _openWindows.Add(model, new WeakReference<Form>(form));
        form.FormClosed += (_, _) => _openWindows.Remove(model);
        form.Show();
    }

    public void Close(IFormModel model)
    {
        var hasWindow = _openWindows[model].TryGetTarget(out var form);
        if (!hasWindow) return;

        form!.Close();
        _openWindows.Remove(model);
    }

    public void Run<TFormModel>() where TFormModel : class, IFormModel
    {
        try
        {
            var form = GetFormFor<TFormModel>(out var model)
                       ?? throw new Exception($"Cannot find the form for ${typeof(TFormModel)}");

            _openWindows.Add(model, new WeakReference<Form>(form));
            form.FormClosed += (_, _) => _openWindows.Remove(model);

            Application.Run(form);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public bool IsFormOpen(IFormModel model)
    {
        return _openWindows.ContainsKey(model);
    }
}