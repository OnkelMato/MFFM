using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using LinkManager48.Messages;
using LinkManager48.Models;
using Mffm.Commands;
using Mffm.Contracts;
using Mffm.Core;

namespace LinkManager48.FormModels
{
    internal class LinkDetailControlModel : IExtendedFormModel<MyLink>, INotifyPropertyChanged, IHandle<CategoryAddedMessage>
    {
        private readonly ILinkRepository _repository;
        private readonly IEventAggregator _eventAggregator;

        public LinkDetailControlModel(ILinkRepository repository, IEventAggregator eventAggregator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.Subscribe(this);

            SaveCommand = new FunctionToCommandAdapter(SaveLink);
            DeleteLinkCommand = new FunctionToCommandAdapter(DeleteLink);
            LinkCategoryItems = new List<string> { string.Empty }
                .Concat(_repository.GetLinks().Select(x => x.Category))
                .Distinct()
                .Where(x => x != null)
                .OrderBy(x => x).ToArray();
        }

        public ICommand DeleteLinkCommand { get; set; }

        private void DeleteLink(object obj)
        {
            _repository.Delete(_context);
            _eventAggregator.Publish(new LinkChangedMessage(_context, LinkChangedMessage.TypeOfChange.Deleted));
        }

        public ICommand SaveCommand { get; set; }

        private void SaveLink(object obj)
        {
            _context.Link = LinkUrl;
            _context.Title = LinkTitle;
            _context.Category = LinkCategory;

            _repository.SaveOrUpdate(_context);
            _eventAggregator.Publish(new LinkChangedMessage(_context, LinkChangedMessage.TypeOfChange.Changed));
        }

        private string _linkUrl;
        private string _linkTitle;
        private string _linkCategory;
        private MyLink _context;
        private string[] _linkCategoryItems;

        public string LinkUrl
        {
            get => _linkUrl;
            set
            {
                _linkUrl = value;
                OnPropertyChanged(nameof(LinkUrl));
            }
        }
        public string LinkTitle
        {
            get => _linkTitle;
            set
            {
                _linkTitle = value;
                OnPropertyChanged(nameof(LinkTitle));
            }
        }
        public string LinkCategory
        {
            get => _linkCategory;
            set
            {
                _linkCategory = value;
                OnPropertyChanged(nameof(LinkCategory));
            }
        }

        public string[] LinkCategoryItems
        {
            get => _linkCategoryItems;
            set => SetField(ref _linkCategoryItems, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
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

        public MyLink Context
        {
            get => _context;
            set
            {
                _context = value;
                LinkTitle = value?.Title;
                LinkUrl = value?.Link;
                LinkCategory = value?.Category;
            }
        }

        public DialogResult Result { get; set; } = DialogResult.None;

        public Task HandleAsync(CategoryAddedMessage message, CancellationToken cancellationToken)
        {
            LinkCategoryItems = LinkCategoryItems.Concat(new[] { message.CategoryName }).ToArray();
            return Task.CompletedTask;
        }
    }
}
