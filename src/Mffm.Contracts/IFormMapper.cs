namespace Mffm.Contracts;

/// <summary>
/// Interface for mapping form models to their corresponding form types.
/// </summary>
public interface IFormMapper
{
    /// <summary>
    /// Gets the form type associated with the specified form model type.
    /// </summary>
    /// <typeparam name="TFormModel">The type of the form model.</typeparam>
    /// <returns>The type of the form associated with the specified form model type.</returns>
    Type GetFormFor<TFormModel>() where TFormModel : class, IFormModel;
}