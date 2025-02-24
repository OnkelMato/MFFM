using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mffm.Contracts;

namespace LinkManager48
{
    internal class MainFormModel : IFormModel, INotifyPropertyChanged
    {
        public string Title { get; set; }

        public MainFormModel(IEventAggregator eventAggregator, IWindowManager windowManager, MenuStripFavouriteManager menuStripManager)
        {
            // todo  windowManager.AttachToForm(menuStripManager, this);
        }

        #region NotofyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }

    internal class MenuStripFavouriteManager:
    // todo : IFormAdapter,
        IHandle<LinkChangedMessage>
    {
        private ToolStripItem _menu;

        public MenuStripFavouriteManager(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void InitializeWith(Form form)
        {
            _menu = form.MainMenuStrip.Items.Find("linksMenu", true).First();
        }

        public Task HandleAsync(LinkChangedMessage message, CancellationToken cancellationToken)
        {
            if (message.ChangeType == ChangeType.Created)
            {
                var item = new ToolStripMenuItem("New Link");
                _menu?.Container?.Add(item);
            }

            return Task.CompletedTask;
        }
    }

}