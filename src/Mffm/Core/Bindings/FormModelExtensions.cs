using Mffm.Contracts;

namespace Mffm.Core.Bindings;

internal static class FormModelExtensions
{
    internal static bool TryFindProperty(this IFormModel formModel, string propertyName)
    {
        propertyName = formModel.GetType().GetProperty(propertyName)?.Name;
        return propertyName is not null;
    }
}