using System.ComponentModel;
using Mffm.Contracts;

namespace Mffm.Core.Bindings
{
    internal class TreeViewBinding : IControlBinding
    {
        #region Binding Adapter

        /// <summary>
        /// Binds a TreeView to a BindingSource.
        /// cf: stolen, I mean taken from https://schreibermartin.wordpress.com/2014/12/17/winforms-treeview-data-binding-part-2-of-2/
        /// </summary>
        /// <typeparam name="TDataItem"></typeparam>
        private class TreeViewBindingAdapter<TDataItem> where TDataItem : class
        {
            private readonly TreeView _treeView;
            private readonly TreeNodeCollection _treeNodeCollection;
            private readonly BindingSource _bindingSource;
            private readonly Func<object, TDataItem> _getDataItemFunc;
            private readonly Func<TDataItem, TreeNode> _addTreeNodeFunc;
            private readonly Action<TDataItem, TreeNode> _updateTreeNodeAction;
            private TreeNode _currentAddItem = null!;
            private readonly TreeNode _parentTreeNode;

            public TreeViewBindingAdapter(
                TreeView treeView,
                BindingSource bindingSource,
                Func<object, TDataItem> getDataItemFunc,
                Func<TDataItem, TreeNode> addTreeNodeFunc,
                Action<TDataItem, TreeNode> updateTreeNodeAction
            ) : this(treeView, treeView.Nodes, null, bindingSource,
                getDataItemFunc, addTreeNodeFunc, updateTreeNodeAction
            )
            { }

            public TreeViewBindingAdapter(
                TreeNode parentTreeNode,
                BindingSource bindingSource,
                Func<object, TDataItem> getDataItemFunc,
                Func<TDataItem, TreeNode> addTreeNodeFunc,
                Action<TDataItem, TreeNode> updateTreeNodeAction
            ) : this(parentTreeNode.TreeView, parentTreeNode.Nodes,
                parentTreeNode, bindingSource,
                getDataItemFunc, addTreeNodeFunc, updateTreeNodeAction
            )
            { }

            private TreeViewBindingAdapter(
                TreeView treeView,
                TreeNodeCollection treeNodeCollection,
                TreeNode parentTreeNode,
                BindingSource bindingSource,
                Func<object, TDataItem> getDataItemFunc,
                Func<TDataItem, TreeNode> addTreeNodeFunc,
                Action<TDataItem, TreeNode> updateTreeNodeAction
            )
            {
                _treeView = treeView ?? throw new ArgumentNullException(nameof(treeView));
                _treeNodeCollection = treeNodeCollection ?? throw new ArgumentNullException(nameof(treeNodeCollection));
                _parentTreeNode = parentTreeNode; // may be null.
                _bindingSource = bindingSource ?? throw new ArgumentNullException(nameof(bindingSource));
                _getDataItemFunc = getDataItemFunc ?? throw new ArgumentNullException(nameof(getDataItemFunc));
                _addTreeNodeFunc = addTreeNodeFunc ?? throw new ArgumentNullException(nameof(addTreeNodeFunc));
                _updateTreeNodeAction = updateTreeNodeAction ?? throw new ArgumentNullException(nameof(updateTreeNodeAction));

                // sync to binding source's current items and selection.
                AddExistingItems();
                _bindingSource.ListChanged += (s, e) =>
                {
                    switch (e.ListChangedType)
                    {
                        case ListChangedType.ItemAdded:
                            AddItem(e.NewIndex);
                            SelectItem();
                            break;
                        case ListChangedType.ItemChanged:
                            UpdateItem();
                            break;
                        case ListChangedType.ItemDeleted:
                            DeleteItem(e.NewIndex);
                            break;
                        case ListChangedType.ItemMoved:
                            MoveItem(e.OldIndex, e.NewIndex);
                            break;
                        case ListChangedType.PropertyDescriptorAdded:
                        case ListChangedType.PropertyDescriptorChanged:
                        case ListChangedType.PropertyDescriptorDeleted:
                            break;
                        case ListChangedType.Reset:
                            _treeNodeCollection.Clear();
                            AddExistingItems();
                            SelectItem();
                            break;
                    }
                };
                _bindingSource.PositionChanged += (s, e) => SelectItem();
                _treeView.AfterSelect += AfterNodeSelect;
                SelectItem();
            }

            private void AfterNodeSelect(object sender, TreeViewEventArgs e)
            {
                var treeNode = e.Node;
                // Skip, if the TreeNode belongs to a foreign collection.
                if (treeNode.Parent != _parentTreeNode) return;
                _bindingSource.Position = treeNode.Index;
            }

            private void AddExistingItems()
            {
                foreach (var listItem in _bindingSource.List)
                {
                    var dataItem = _getDataItemFunc(listItem);
                    var treeNode = _addTreeNodeFunc(dataItem);
                    if (treeNode == null) continue;
                    _updateTreeNodeAction(dataItem, treeNode);

                    if (!_treeNodeCollection.Contains(treeNode))
                        _treeNodeCollection.Add(treeNode);
                }
            }

            private void AddItem(int newIndex)
            {
                var dataItem = _getDataItemFunc(_bindingSource[newIndex]);
                if (_currentAddItem == null)
                {
                    var treeNode = _addTreeNodeFunc(dataItem);
                    if (treeNode == null || _treeNodeCollection.Contains(treeNode)) return;

                    _treeNodeCollection.Insert(newIndex, treeNode);
                    _currentAddItem = treeNode;
                    return;
                }

                _updateTreeNodeAction(dataItem, _currentAddItem);
                _currentAddItem = null!;
            }

            public void UpdateItem()
            {
                if (_bindingSource.Current == null) return;
                var dataItem = _getDataItemFunc(_bindingSource.Current);
                var treeNode = _treeNodeCollection[_bindingSource.Position];
                _updateTreeNodeAction(dataItem, treeNode);
            }

            private void DeleteItem(int index)
            {
                _treeNodeCollection.RemoveAt(index);
                _currentAddItem = null!;
            }

            private void MoveItem(int oldIndex, int newIndex)
            {
                var treeNode = _treeNodeCollection[_bindingSource.Position];
                _treeNodeCollection.RemoveAt(oldIndex);
                _treeNodeCollection.Insert(newIndex, treeNode);
            }

            private void SelectItem()
            {
                if (_bindingSource.Position < 0) return;
                if (_treeNodeCollection.Count <= _bindingSource.Position) return;
                var treeNode = _treeNodeCollection[_bindingSource.Position];
                treeNode.EnsureVisible();
                _treeView.SelectedNode = treeNode;
            }
        }

        #endregion

        private const string Selected = "Selected";

        public bool Bind(Control control, IFormModel formModel)
        {
            if (!(control is TreeView treeView)) return false;

            treeView.BeginUpdate();

            var lst = formModel.GetType().GetProperty(treeView.Name)?.GetValue(formModel) as BindingList<TreeViewNodeModel>;

            // bind all the nodes recursively. Yes, it is an indirect recursion in the getDataItemFunc<>
            _ = new TreeViewBindingAdapter<TreeViewNodeModel>(
                treeView,
                new BindingSource() { DataSource = lst },
                item => item as TreeViewNodeModel,
                item =>
                {
                    var node = new TreeNode(item.Text) { Tag = item };
                    treeView.Nodes.Add(node);

                    _ = new TreeViewBindingAdapter<TreeViewNodeModel>(
                        node,
                        new BindingSource() { DataSource = item.Children },
                        child => child as TreeViewNodeModel,
                        child =>
                        {
                            var childNode = new TreeNode(child.Text) { Tag = child };
                            node.Nodes.Add(childNode);
                            return childNode;
                        },
                        (child, childNode) =>
                        {
                            childNode.Text = child.Text;
                            childNode.Tag = child;
                        }
                    );

                    return node;
                },
                (item, treeNode) =>
                {
                    treeNode.Text = item.Text;
                    treeNode.Tag = item;
                });

            // this is the trick to bind the selected node.
            var selectedFieldName = treeView.Name + Selected;
            if (formModel.GetType().GetProperty(selectedFieldName) != null)
                treeView.AfterSelect += (sender, args) =>
                    formModel.GetType().GetProperty(selectedFieldName)?.SetValue(formModel, args.Node.Tag as TreeViewNodeModel);

            treeView.EndUpdate();

            return true;
        }
    }
}