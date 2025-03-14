using System.Windows.Input;
using WinFormsMffmPrototype.Core.Logging;
using WinFormsMffmPrototype.MvvmFramework;

namespace WinFormsMffmPrototype.Ui.EditUser;

public class EditFormModel : IFormModel
{
    [Obsolete("only for design time")]
    public EditFormModel()
    {
    }

    public EditFormModel(IMffmLogger logger, CloseFormCommand closeFormCommand)
    {
        CloseForm = closeFormCommand ?? throw new ArgumentNullException(nameof(closeFormCommand));

        this.Firstname = "Thomas";
        this.Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int ZipCode { get; set; }

    public ICommand CloseForm { get; private set; }
    public ICommand SavePerson { get; private set; }
    public IFormModel FormModel => this;
}