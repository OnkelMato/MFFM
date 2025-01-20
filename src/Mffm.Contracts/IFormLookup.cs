namespace Mffm.Contracts;

public interface IFormLookup
{
    Type GetFormFor<TFormModel>() where TFormModel : class, IFormModel;

    Type[] GetForms();
    Type[] GetFormModels();
}