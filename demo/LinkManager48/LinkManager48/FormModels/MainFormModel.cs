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
    internal class MainFormModel : Mffm.Contracts.IFormModel, INotifyPropertyChanged, IHandle<CategoryAddedMessage>
    {
        public CreateCategoryCommand CreateCategory { get; }
        private TreeViewNodeModel _linkTreeViewNodeSelected;
        private string _title = Constants.AppName;
        private BindingList<TreeViewNodeModel> _linkTreeView;
        private TreeViewNodeModel _coreTreeViewSelected;
        private LinkDetailControlModel _selectedLink;

        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        public MainFormModel(
            IEventAggregator eventAggregator, IWindowManager windowManager,
            MainFormMenuLinkManager mainFormMenuStripManager, LinkDragAndDropManager linkDragAndDrop,
            ILinkRepository linkRepository, CreateCategoryCommand createCategoryCommand)
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
            SelectedLink = new LinkDetailControlModel();
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
    }
}