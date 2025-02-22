using Mffm.Contracts;

namespace Mffm.Core;

// this can be internal because we use a builder pattern to create it,
// and we don't want to expose it to the outside world
internal class DefaultFormMapper(Dictionary<Type, Type> formModelToFormMapping) : IFormMapper
{
    private readonly Dictionary<Type, Type> _formModelToFormMapping = formModelToFormMapping ?? throw new ArgumentNullException(nameof(formModelToFormMapping));

    public Type GetFormFor<TFormModel>() where TFormModel : class, IFormModel
    {
        return _formModelToFormMapping[typeof(TFormModel)];
    }
}