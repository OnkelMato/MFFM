using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LinkManager48.MffmExtensions;
using LinkManager48.Models;
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

        public MainFormModel(
            IEventAggregator eventAggregator, IWindowManager windowManager,
            MainFormMenuLinkManager mainFormMenuStripManager, LinkDragAndDropManager linkDragAndDrop,
            ILinkRepository linkRepository)
        {
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
    }


    internal class LinkDragAndDropManager : IFormAdapter
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILinkRepository _repository;

        public LinkDragAndDropManager(IEventAggregator eventAggregator, ILinkRepository repository)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void InitializeWith(Form form)
        {
            form.AllowDrop = true;
            form.DragEnter += (sender, args) =>
            {
                if (args.Data.GetDataPresent(DataFormats.Text))
                    args.Effect = DragDropEffects.Copy;
            };
            form.DragDrop += (sender, args) =>
            {
                if (args.Data.GetDataPresent(DataFormats.Text))
                {
                    var link = args.Data.GetData(DataFormats.Text).ToString();

                    var linkModel = new MyLink(link, link);
                    _repository.SaveOrUpdate(linkModel);
                    //MessageBox.Show(link);
                }
            };
        }
    }
}