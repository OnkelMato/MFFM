using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LinkManager48.MffmExtensions
{
    internal class TreeViewNodeModel : INotifyPropertyChanged
    {
        private object _data;
        private string _text;
        private BindingList<TreeViewNodeModel> _children = new BindingList<TreeViewNodeModel>();

        public virtual string Text
        {
            get => _text;
            set => SetField(ref _text, value);
        }

        public virtual BindingList<TreeViewNodeModel> Children
        {
            get => _children;
            set => SetField(ref _children, value);
        }

        public virtual object Data
        {
            get => _data;
            set => SetField(ref _data, value);
        }

        #region INotifyPropertyChanged

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