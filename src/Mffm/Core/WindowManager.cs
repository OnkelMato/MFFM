using Mffm.Contracts;

namespace Mffm.Core;

/// <summary>
///     The windows manager is responsible for WinForms form management and creation. It
///     knows about windows forms internal functions like Show() or ShowDialog().
///     The dictionary _openWindows is used to keep track of the open windows. An event
///     handler is added to the FormClosed event of the form to remove the form from the
///     dictionary.
/// </summary>
/// <param name="serviceProvider"></param>
/// <param name="bindingManager"></param>
/// <param name="formMapper"></param>
internal class WindowManager(IServiceProvider serviceProvider, IBindingManager bindingManager, IFormMapper formMapper) : IWindowManager
{
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    private readonly IBindingManager _bindingManager = bindingManager ?? throw new ArgumentNullException(nameof(bindingManager));
    private readonly IFormMapper _formMapper = formMapper ?? throw new ArgumentNullException(nameof(formMapper));

    // keep track of all open windows so we can close them
    private readonly Dictionary<IFormModel, WeakReference<Form>> _openWindows = new();


    private Form GetFormFor<TFormModel>(object? context = null)
        where TFormModel : class, IFormModel
    {
        // get form model and set the context
        var ctor = typeof(TFormModel)
            .GetConstructors()
            .OrderByDescending(c => c.GetParameters().Length)
            .First();

        var parameters = ctor
            .GetParameters()
            .Select(param =>
            {
                var ctorParam = _serviceProvider.GetService(param.ParameterType);
                if (ctorParam is null) throw new ServiceNotFoundException($"Cannot find service for ${param.ParameterType.Name}");
                return ctorParam;
            });

        IFormModel formModel = (ctor.Invoke(parameters.ToArray()) as TFormModel) ?? throw new ServiceNotFoundException($"Cannot find service for ${typeof(TFormModel).Name}");
        formModel.GetType().GetProperty(MffmConstants.ContextPropertyName)?.SetValue(formModel, context);

        // form mapper is responsible to getting the form for the form model
        var formType = _formMapper.GetFormFor<TFormModel>();
        var form = _serviceProvider.GetService(formType) as Form;
        if (form is null) throw new ServiceNotFoundException($"Cannot find service for ${formType.Name}");

        // as we have the form now, we can attach the constructor IFormAdapter to the form
        foreach (var parameter in parameters)
            if (parameter is IFormAdapter formAdapter) formAdapter.InitializeWith(form!);

        // track forms and formModels
        _openWindows.Add(formModel, new WeakReference<Form>(form));
        form.FormClosed += (_, _) => _openWindows.Remove(formModel);

        // binding manager is responsible for the data binding and connection between the FormModel and the Form
        _bindingManager.CreateBindings(formModel, form);

        // we can do the subscriber automatically here. No need in the form model
        (_serviceProvider.GetService(typeof(IEventAggregator)) as IEventAggregator)?.Subscribe(formModel);

        return form;
    }

    public void Show<TFormModel>(object? context) where TFormModel : class, IFormModel
    {
        var form = GetFormFor<TFormModel>(context);
        if (form is null) throw new Exception($"Cannot find the form for ${typeof(TFormModel)}");

        form.Show();
    }

    public DialogResult ShowModal<TFormModel>(object? context = null) where TFormModel : class, IFormModel
    {
        var form = GetFormFor<TFormModel>(context);
        if (form is null) throw new Exception($"Cannot find the form for ${typeof(TFormModel)}");

        return form.ShowDialog();
    }

    public bool Close(IFormModel model, DialogResult? dialogResult = null)
    {
        var hasWindow = _openWindows[model].TryGetTarget(out var form);
        if (!hasWindow) return false;

        //set the dialog result from model (optional property) and override it with the parameter
        var dialogResultProperty = (DialogResult?)(model.GetType().GetProperty(MffmConstants.DialogResultPropertyName)?.GetValue(model));
        if (dialogResultProperty != null) form!.DialogResult = dialogResultProperty.Value; // set the DialogResult property from the model
        if (dialogResult != null) form!.DialogResult = dialogResult.Value; // override the setting from property

        form!.Close();

        _openWindows.Remove(model);
        return true;
    }

    /// <summary>
    /// Run is used to initially run the application. This is required as entry point.
    /// </summary>
    /// <typeparam name="TFormModel"></typeparam>
    /// <param name="context">Context that </param>
    public DialogResult Run<TFormModel>(object? context = null) where TFormModel : class, IFormModel
    {
        var form = GetFormFor<TFormModel>(context) ?? throw new Exception($"Cannot find the form for ${typeof(TFormModel)}");

        Application.Run(form);

        return form.DialogResult;
    }

    public bool IsFormOpen(IFormModel model)
    {
        return _openWindows.ContainsKey(model);
    }

    //public void AttachToForm(IFormAdapter formAdapter, Form form)
    //{
    //    // it is a bit complicated here, as this is usually called during a constructor call. 
    //    // so in that case the windows is not in dictionary, we need to wait until it is added
    //    if (!_openWindows.TryGetValue(formModel, out var window))
    //    {
    //        _deferredExecutionActions.Add(new Tuple<IFormModel, Action>(formModel, () => AttachToForm(formAdapter, formModel)));
    //        return;
    //    }

    //    var hasWindow = window.TryGetTarget(out var form);
    //    if (!hasWindow) return;

    //}
}