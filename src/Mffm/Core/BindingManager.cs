using Mffm.Contracts;

namespace Mffm.Core;

public class BindingManager(IServiceProvider serviceProvider, IFormLookup formLookup) : IBindingManager
{
    private readonly IFormLookup _formLookup = formLookup ?? throw new ArgumentNullException(nameof(formLookup));

    private readonly IServiceProvider _serviceProvider =
        serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    public Form GetFormFor<TFormModel>(out TFormModel formModel)
        where TFormModel : class, IFormModel
    {
        formModel = _serviceProvider.GetService(typeof(TFormModel)) as TFormModel
                    ?? throw new ServiceNotFoundException($"Cannt fond service for ${typeof(TFormModel).Name}");
        var formType = _formLookup.GetFormFor<TFormModel>();
        var form = _serviceProvider.GetService(formType) as Form;

        // todo maybe check for null
        AttachBinding(formModel!, form!);

        return form!;
    }

    private void AttachBinding(IFormModel formModel, Form form)
    {
        // todo here we should use a strategy pattern for the different controls

        foreach (var property in formModel.GetType().GetProperties())
        {
            var control = form.Controls.Find(property.Name, true).FirstOrDefault();
            if (control is Button button)
            {
                // we have to bind the commandparameter first to have it passed during the binding of the command itself
                button.DataBindings.Add(new Binding(nameof(button.CommandParameter), formModel, null, true,
                    DataSourceUpdateMode.Never));
                button.DataBindings.Add(new Binding(nameof(button.Command), formModel, property.Name, true,
                    DataSourceUpdateMode.OnPropertyChanged));
                continue;
            }

            var menuItem = form.MainMenuStrip?.Items?.Find(property.Name, true)?.FirstOrDefault();
            if (menuItem is not null)
            {
                // we have to bind the commandparameter first to have it passed during the binding of the command itself
                menuItem.DataBindings.Add(new Binding(nameof(button.CommandParameter), formModel, null, true,
                    DataSourceUpdateMode.Never));
                menuItem.DataBindings.Add(new Binding(nameof(button.Command), formModel, property.Name, true,
                    DataSourceUpdateMode.OnPropertyChanged));
                continue;
            }

            if (control == null) continue;
            // it just randomly binds to the text
            var binding = new Binding(nameof(control.Text), formModel, property.Name, true,
                DataSourceUpdateMode.OnPropertyChanged);
            control.DataBindings.Add(binding);
        }
    }
}