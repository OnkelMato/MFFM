using System.Windows.Input;
using Mffm.Commands;
using Mffm.Contracts;
using Mffm.Samples.Core.Logging;
using Mffm.Samples.Extensions.GeoComponent;

namespace Mffm.Samples.Ui.EditUser;

public class EditFormModel : IFormModel
{
    public EditFormModel(IBmLogger logger, CloseFormCommand closeFormCommand, SavePersonCommand savePersonCommand)
    {
        Close = closeFormCommand ?? throw new ArgumentNullException(nameof(closeFormCommand));
        Save = savePersonCommand ?? throw new ArgumentNullException(nameof(savePersonCommand));

        SaveAndClose = new CompositeCommand(savePersonCommand, closeFormCommand);

        Firstname = "Onkel";
        Lastname = "Mato";
        Address = "Matostrasse 1";
        City = "Mato City";
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int ZipCode { get; set; }

    public Coordinate Coordinate { get; set; } = new Coordinate() { Latitude = 7.3, Longitude = 53.2 };

    /// <summary>
    ///     Close command provided by the MFFM framework
    /// </summary>
    public ICommand Close { get; private set; }

    /// <summary>
    ///     Save command for person
    /// </summary>
    public ICommand Save { get; private set; }

    /// <summary>
    ///     Save command for person
    /// </summary>
    public ICommand SaveAndClose { get; private set; }
}