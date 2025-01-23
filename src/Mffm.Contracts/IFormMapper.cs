namespace Mffm.Contracts;

public interface IFormMapper
{
    Type GetFormFor<TFormModel>() where TFormModel : class, IFormModel;
}