using Mffm.Contracts;

namespace Mffm.Core.Bindings;

internal static class BindingExtensions
{
    internal static bool HasNoBindingFor(this BindingsCollection source, string propertyName)
    {
       return !HasBindingFor(source, propertyName);
    }

    internal static bool HasBindingFor(this BindingsCollection source, string propertyName)
    {
        return source.Cast<Binding>()
            .Any(dataBinding => dataBinding.PropertyName == propertyName);
    }

    internal static bool TryAddBinding(this ControlBindingsCollection source,
        string controlProperty, IFormModel formModel, string formModelProperty,
        bool allowFormatting = true, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged)
    {
        // check if binding already exists. This is important when more than one IBinding is applied to the same controlProperty.
        if (source.HasBindingFor(controlProperty)) return false;

        source.Add(new Binding(controlProperty, formModel, formModelProperty, allowFormatting, updateMode));

        return true;
    }
}