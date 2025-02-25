using System;
using System.Collections.Generic;
using System.ComponentModel;
 using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace LinkManager48.MffmExtensions
{
    internal class BindableTreeView : TreeView, INotifyPropertyChanged
    {
        private BindingList<BindableTreeViewModel> _rootModels;

        public BindableTreeView()
        {
            this.AfterSelect += BindableTreeView_AfterSelect;
        }

        private void BindableTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedModel));
        }

        [Bindable(true)]
        public BindingList<BindableTreeViewModel> RootModels
        {
            get => _rootModels;
            set
            {
                if (!SetField(ref _rootModels, value)) return;
                ResetNodes();
            }
        }

        [Bindable(true)]
        public BindableTreeViewModel SelectedModel
        {
            get => SelectedNode?.Tag as BindableTreeViewModel;
            set => SelectedNode = FindNode(Nodes, value);
        }

        private static TreeNode FindNode(TreeNodeCollection nodes, BindableTreeViewModel model)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag == model)
                    return node;

                var found = FindNode(node.Nodes, model);
                if (found != null)
                    return found;
            }

            return null;
        }

        private void ResetNodes()
        {
            //_rootModels.ListChanged -= ListChanged;
            //foreach (var model in _rootModels)
            //{
            //    // todo fix recursively
            //    // or change this entirely to a flat list with a parent indicator
            //    model.Children.ListChanged -= ListChanged;
            //}

            Nodes.Clear();
            if (_rootModels == null) return;
            foreach (var model in _rootModels)
            {
                AddNode(Nodes, model);
                //model.Children.ListChanged += ListChanged;
            }

            //_rootModels.ListChanged += ListChanged;
        }

        private void ListChanged(object sender, ListChangedEventArgs e)
        {
            //switch (e.ListChangedType)
            //{
            //    case ListChangedType.Reset:
            //        break;
            //    case ListChangedType.ItemAdded:
            //        Nodes.Add(new TreeNode(_rootModels))
            //        break;
            //    case ListChangedType.ItemDeleted:
            //        break;
            //    case ListChangedType.ItemMoved:
            //        break;
            //    case ListChangedType.ItemChanged:
            //        break;
            //    case ListChangedType.PropertyDescriptorAdded:
            //        break;
            //    case ListChangedType.PropertyDescriptorDeleted:
            //        break;
            //    case ListChangedType.PropertyDescriptorChanged:
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}
        }

        private void AddNode(TreeNodeCollection nodes, BindableTreeViewModel bindableTreeViewModel)
        {
            var node = new TreeNode(bindableTreeViewModel.Text) { Tag = bindableTreeViewModel };
            nodes.Add(node);
            if (bindableTreeViewModel.Children == null) return;

            foreach (var child in bindableTreeViewModel.Children)
                AddNode(node.Nodes, child);
        }

        #region NotiftPropertyChanged

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