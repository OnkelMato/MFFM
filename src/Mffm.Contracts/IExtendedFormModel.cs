namespace Mffm.Contracts;

/// <summary>
/// Form model with all supported properties. Can be used to be more compiler safe
/// </summary>
public interface IExtendedFormModel<TFormModelContext> : IFormModel
    where TFormModelContext: class
{
    /// <summary>
    /// Context instance which is used to pass data between form models. Make sure you don't change the instance!
    /// </summary>
    TFormModelContext Context { get; set; }

    /// <summary>
    /// Dialog result to return a result from a dialog. Only works with modal dialogs, obviously
    /// </summary>
    DialogResult Result { get; set; }
}