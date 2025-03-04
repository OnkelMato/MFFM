using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using LinkManager48.FormModels.Commands;
using LinkManager48.MffmExtensions;
using LinkManager48.Models;
using Mffm.Contracts;

namespace LinkManager48.FormModels
{
    internal class MainFormModel :
        IFormModel,
        INotifyPropertyChanged,
        IHandle<CategoryAddedMessage>,
        IHandle<LinkChangedMessage>
    {
        public CreateCategoryCommand CreateCategory { get; }
        private string _title = Constants.AppName;
        private TreeViewNodeModel _coreTreeViewSelected;
        private LinkDetailControlModel _selectedLink;

        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        public MainFormModel(
            IEventAggregator eventAggregator, IWindowManager windowManager, ILinkRepository linkRepository,
            MainFormMenuLinkManager mainFormMenuStripManager, LinkDragAndDropManager linkDragAndDrop,
            CreateCategoryCommand createCategoryCommand,
            LinkDetailControlModel selectedLink)
        {
            CreateCategory = createCategoryCommand ?? throw new ArgumentNullException(nameof(createCategoryCommand));
            eventAggregator.Subscribe(this);

            // we use a menu strip manager to handle the menu items. 
            windowManager.AttachToForm(mainFormMenuStripManager, this);
            windowManager.AttachToForm(linkDragAndDrop, this);

            // get all links and categories.
            var allLinks = linkRepository.GetLinks().ToArray();
            var categories = allLinks.Select(x => x.Category ?? "_").Distinct();

            // create nodes for each category.
            var nodes = categories.ToDictionary(category => category, category => new TreeViewNodeModel() { Text = category });

            foreach (var link in allLinks)
                nodes[link.Category ?? "_"].Children.Add(new TreeViewNodeModel()
                {
                    Text = link.Title,
                    Data = link
                });

            CoreTreeView = new BindingList<TreeViewNodeModel>(nodes.Values.ToList());
            SelectedLink = selectedLink;
        }


        //########################################################

        public BindingList<TreeViewNodeModel> CoreTreeView { get; set; }

        public TreeViewNodeModel CoreTreeViewSelected
        {
            get => _coreTreeViewSelected;
            set
            {
                SetField(ref _coreTreeViewSelected, value);
                Title = (value?.Text ?? "<nil>") + $" [{Constants.AppName}]";
                SelectedLink.Context = value?.Data as MyLink;
            }
        }

        //########################################################

        public LinkDetailControlModel SelectedLink
        {
            get => _selectedLink;
            set => SetField(ref _selectedLink, value);
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

        public Task HandleAsync(CategoryAddedMessage message, CancellationToken cancellationToken)
        {
            CoreTreeView.Add(new TreeViewNodeModel() { Text = message.CategoryName });
            //OnPropertyChanged(nameof(LinkTreeView));
            return Task.CompletedTask;
        }

        public Task HandleAsync(LinkChangedMessage message, CancellationToken cancellationToken)
        {
            switch (message.ChangeType)
            {
                case LinkChangedMessage.TypeOfChange.Created:
                    var categoryNode = CoreTreeView.FirstOrDefault(x => x.Text == message.Link.Category);
                    if (categoryNode == null)
                    {
                        categoryNode = new TreeViewNodeModel() { Text = message.Link.Category };
                        CoreTreeView.Add(categoryNode);
                    }
                    categoryNode.Children.Add(new TreeViewNodeModel() { Text = message.Link.Title, Data = message.Link });
                    break;

                case LinkChangedMessage.TypeOfChange.Changed:
                    var node = CoreTreeView
                        .SelectMany(x => x.Children)
                        .First(x => ((MyLink)x.Data).Id == message.Link.Id);

                    // check if we need to change the parent node
                    var catNode = CoreTreeView.First(x => message.Link.Category == x.Text);
                    if (!catNode.Children.Contains(node))
                    {
                        var oldCategoryNode = CoreTreeView.First(x => x.Children.Contains(node));
                        oldCategoryNode.Children.Remove(node);
                        catNode.Children.Add(node);
                    }

                    // let's update the node
                    node.Data = message.Link;
                    node.Text = message.Link.Title;
                    break;

                case LinkChangedMessage.TypeOfChange.Deleted:
                    var removeNode = CoreTreeView
                        .SelectMany(x => x.Children)
                        .First(x => ((MyLink)x.Data).Id == message.Link.Id);
                    var removeCategory = CoreTreeView.First(x => x.Children.Contains(removeNode));
                    removeCategory.Children.Remove(removeNode);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Task.CompletedTask;
        }
    }
}