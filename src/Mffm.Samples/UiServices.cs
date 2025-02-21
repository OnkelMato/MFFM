using System.Collections;
using System.Windows.Forms.Design;

namespace Mffm.Samples
{
    /// <summary>
    ///     Required for the designer to work
    /// </summary>
    public class UiServices : IUIService
    {
        public bool CanShowComponentEditor(object component)
        {
            return false;
        }

        public IWin32Window GetDialogOwnerWindow()
        {
            return null;
        }

        public void SetUIDirty()
        {
        }

        public bool ShowComponentEditor(object component, IWin32Window parent)
        {
            return false;
        }

        public DialogResult ShowDialog(Form form)
        {
            return DialogResult.None;
        }

        public void ShowError(string message)
        {
        }

        public void ShowError(Exception ex)
        {
        }

        public void ShowError(Exception ex, string message)
        {
        }

        public void ShowMessage(string message)
        {
        }

        public void ShowMessage(string message, string caption)
        {
        }

        public DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons)
        {
            return DialogResult.None;
        }

        public bool ShowToolWindow(Guid toolWindow)
        {
            return false;
        }

        public IDictionary Styles { get; }
    }
}