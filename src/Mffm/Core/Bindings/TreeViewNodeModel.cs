using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mffm.Core.Bindings
{
    /// <summary>
    /// Model for binding data to a tree view.
    /// </summary>
    public class TreeViewNodeModel : INotifyPropertyChanged
    {
        private object _data = null!;
        private string _text = null!;
        private BindingList<TreeViewNodeModel> _children = [];

        /// <summary>
        /// Text of the node
        /// </summary>
        public virtual string Text
        {
            get => _text;
            set => SetField(ref _text, value);
        }

        /// <summary>
        /// Children of the node
        /// </summary>
        public virtual BindingList<TreeViewNodeModel> Children
        {
            get => _children;
            set => SetField(ref _children, value);
        }

        /// <summary>
        /// Data that os represented by the node
        /// </summary>
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