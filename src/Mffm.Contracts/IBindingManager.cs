namespace Mffm.Contracts;

/// <summary>
///     The binding manager is responsible for the data binding and connection between the FormModel and the Form
/// </summary>
public interface IBindingManager
{
    Form GetFormFor<TFormModel>(out TFormModel formModel) where TFormModel : class, IFormModel;
}