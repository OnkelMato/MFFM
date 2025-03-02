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
}