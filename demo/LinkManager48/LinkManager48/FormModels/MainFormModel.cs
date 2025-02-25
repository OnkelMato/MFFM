using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using LinkManager48.MffmExtensions;
using LinkManager48.Models;
using Mffm.Commands;
using Mffm.Contracts;
using Mffm.Core;

namespace LinkManager48.FormModels
{
    internal class MainFormModel : IFormModel, INotifyPropertyChanged, IHandle<CategoryAddedMessage>
    {
        public CreateCategoryCommand CreateCategory { get; }
        private BindableTreeViewModel _linkTreeViewSelected;
        private string _title = Constants.AppName;
        private BindingList<BindableTreeViewModel> _linkTreeView;

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
            var nodes = categories.ToDictionary(category => category, category => new BindableTreeViewModel() { Text = category });

            foreach (var link in allLinks)
                nodes[link.Category ?? "_"].Children.Add(new BindableTreeViewModel() { Text = link.Title });

            LinkTreeView = new BindingList<BindableTreeViewModel>(nodes.Values.ToList());
            //LinkTreeView = new BindingList<BindableTreeViewModel>(
            //    linkRepository
            //        .GetLinks()
            //        .Select(x => new BindableTreeViewModel() { Text = x.Title }).ToList());
            // todo make this more awesome. 
            //LinkTreeView = new BindingList<BindableTreeViewModel>()
            //{
            //    new BindableTreeViewModel() { Text = "Root", Children = 
            //        new BindingList<BindableTreeViewModel>() { new BindableTreeViewModel() { Text = "Child" } } },
            //    new BindableTreeViewModel() { Text = "Root2" }
            //};
            //LinkTreeViewSelected = LinkTreeView[1];
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

        public Task HandleAsync(CategoryAddedMessage message, CancellationToken cancellationToken)
        {
            LinkTreeView.Add(new BindableTreeViewModel() { Text = message.CategoryName });
            OnPropertyChanged(nameof(LinkTreeView));
            return Task.CompletedTask;
        }
    }

    internal class CreateCategoryCommand : ICommand
    {
        private readonly IWindowManager _windowManager;

        public CreateCategoryCommand(IWindowManager windowManager)
        {
            _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _windowManager.ShowModal<CreateCategoryFormModel>();
        }

        public event EventHandler CanExecuteChanged;
    }

    internal class CategoryAddedMessage
    {
        public string CategoryName { get; }

        public CategoryAddedMessage(string categoryName)
        {
            CategoryName = categoryName;
        }
    }

    internal class CreateCategoryFormModel : IFormModel
    {
        private readonly IEventAggregator _eventAggregator;

        public CreateCategoryFormModel(
            ICommandResolver commandResolver,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            Ok = new CompositeCommand(
                new FunctionToCommandAdapter(ctx => PublishCategor()),
                commandResolver.ResolveCommand<CloseFormCommand>());
            Cancel = commandResolver.ResolveCommand<CloseFormCommand>();
        }

        private void PublishCategor()
        {
            _eventAggregator.Publish(new CategoryAddedMessage(CategoryName));
        }

        public string CategoryName { get; set; }

        public ICommand Ok { get; set; }

        public ICommand Cancel { get; set; }
    }
}