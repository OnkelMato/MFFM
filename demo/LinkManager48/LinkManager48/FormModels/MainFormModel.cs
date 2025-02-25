using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LinkManager48.MffmExtensions;
using Mffm.Contracts;

namespace LinkManager48.FormModels
{
    internal class MainFormModel : IFormModel, INotifyPropertyChanged
    {
        private BindableTreeViewModel _linkTreeViewSelected;
        private string _title = Constants.AppName;
        private BindingList<BindableTreeViewModel> _linkTreeView;

        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        public MainFormModel(IEventAggregator eventAggregator, IWindowManager windowManager, MainFormMenuLinkManager mainFormMenuStripManager)
        {
            // we use a menu strip manager to handle the menu items. 
            windowManager.AttachToForm(mainFormMenuStripManager, this);
            
            LinkTreeView = new BindingList<BindableTreeViewModel>()
            {
                new BindableTreeViewModel() { Text = "Root", Children = 
                    new BindingList<BindableTreeViewModel>() { new BindableTreeViewModel() { Text = "Child" } } },
                new BindableTreeViewModel() { Text = "Root2" }
            };
            LinkTreeViewSelected = LinkTreeView[1];
        }

        public BindableTreeViewModel LinkTreeViewSelected
        {
            get => _linkTreeViewSelected;
            set
            {
                SetField(ref _linkTreeViewSelected, value);
                Title = value.Text + $" {Constants.AppName}";
            }
        }

        public BindingList<BindableTreeViewModel> LinkTreeView
        {
            get => _linkTreeView;
            set
            {
                SetField(ref _linkTreeView, value);
                value.ListChanged += (sender, args) =>
                {
                    if (args.ListChangedType == ListChangedType.ItemChanged)
                    {
                        Title = LinkTreeViewSelected.Text + $" {Constants.AppName}";
                    }
                };
            }
        }

        #region NotifyPropertyChanged

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
}