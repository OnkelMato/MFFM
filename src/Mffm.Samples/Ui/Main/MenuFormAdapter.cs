using Mffm.Contracts;

namespace Mffm.Samples.Ui.Main;

/// <summary>
/// this is a form adapter that is used to initialize the form
/// </summary>
public class MenuFormAdapter : IFormAdapter
{
    public void InitializeWith(Form form)
    {
        form.Text = "Menu Form";
        form.FormBorderStyle = FormBorderStyle.FixedDialog;
        form.MaximizeBox = false;
        form.MinimizeBox = false;
        form.StartPosition = FormStartPosition.CenterScreen;
    }
}