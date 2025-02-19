using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mffm.Contracts;
using Mffm.Samples.Properties;
using Mffm.Samples.Ui.EditUser;

namespace Mffm.Samples.Ui.Main
{
    internal class CloseApplicationCommand : IExtendedCommand
    {
        private readonly IWindowManager _windowManager;

        public CloseApplicationCommand(IWindowManager windowManager)
        {
            _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
            Icon = Image.FromStream(new MemoryStream(Resources.icons_loeschen));
            ShortcutKeys = Keys.Control | Keys.Shift| Keys.Q;
        }
        public Image? Icon { get; }
        public Keys? ShortcutKeys { get; }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is not IFormModel model) return;

            _windowManager.Close(model);
        }

        public event EventHandler? CanExecuteChanged;
        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
