using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinkManager48.MffmExtensions;
using LinkManager48.Models;
using Mffm.Contracts;

namespace LinkManager48.FormModels.FormAdapters
{
    internal class MainFormMenuLinkManager : IFormAdapter, IHandle<LinkChangedMessage>, IHandle<CategoryAddedMessage>
    {
        private readonly ILinkRepository _repository;
        private ToolStripDropDown _menu;
        private Dictionary<string, ToolStripMenuItem> _categoryMenuItems;

        public MainFormMenuLinkManager(IEventAggregator eventAggregator, ILinkRepository repository)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            eventAggregator.Subscribe(this);
        }

        public void InitializeWith(Form form)
        {
            _menu = (form.MainMenuStrip.Items.Find("linksToolStripMenuItem", true).FirstOrDefault() as ToolStripMenuItem)?.DropDown;

            InitializeMenu();
        }

        private void InitializeMenu()
        {
            var links = _repository.GetLinks().ToArray();
            var cats = links.Select(x => x.Category).Where(x => !string.IsNullOrEmpty(x)).Distinct().OrderBy(x => x).ToArray();
            _categoryMenuItems = new Dictionary<string, ToolStripMenuItem>();

            var menuItem = new ToolStripMenuItem(@"<empty>"); // new ToolStripDropDown() { Text = @"<empty>" };
            _menu.Items.Add(menuItem);
            _categoryMenuItems.Add(string.Empty, menuItem);
            foreach (var cat in cats)
            {
                menuItem = new ToolStripMenuItem(cat); // new ToolStripDropDown() { Text = @"<empty>" };
                _menu.Items.Add(menuItem);
                _categoryMenuItems.Add(cat, menuItem);
            }

            foreach (var link in links)
            {
                var parent = _categoryMenuItems[link.Category];
                var child = parent.DropDownItems.Add(link.Title);
                child.Tag = link;
                child.Click += (sender, args) => System.Diagnostics.Process.Start(((sender as ToolStripMenuItem)?.Tag as MyLink)?.Link ?? throw new ArgumentNullException());
            }
        }

        public Task HandleAsync(LinkChangedMessage message, CancellationToken cancellationToken)
        {
            switch (message.ChangeType)
            {
                case LinkChangedMessage.TypeOfChange.Created:
                    var link = message.Link;
                    var parent = _categoryMenuItems[link.Category];
                    var child = parent.DropDownItems.Add(link.Title);
                    child.Tag = link;
                    child.Click += (sender, args) => System.Diagnostics.Process.Start(((sender as ToolStripMenuItem)?.Tag as MyLink)?.Link ?? throw new ArgumentNullException());
                    break;

                case LinkChangedMessage.TypeOfChange.Changed:
                    var menuItem = _categoryMenuItems
                        .SelectMany(x => x.Value.DropDownItems.Cast<ToolStripMenuItem>())
                        .First(x => ((MyLink)x.Tag).Id == message.Link.Id);
                    menuItem.Owner.Items.Remove(menuItem);
                    var categoryNode = _categoryMenuItems[message.Link.Category];

                    var newChild = categoryNode.DropDownItems.Add(message.Link.Title);
                    newChild.Tag = message.Link;
                    newChild.Click += (sender, args) => System.Diagnostics.Process.Start(((sender as ToolStripMenuItem)?.Tag as MyLink)?.Link ?? throw new ArgumentNullException());

                    break;

                case LinkChangedMessage.TypeOfChange.Deleted:
                    var removeMenuItem = _categoryMenuItems
                        .SelectMany(x => x.Value.DropDownItems.Cast<ToolStripMenuItem>())
                        .First(x => ((MyLink)x.Tag).Id == message.Link.Id);
                    removeMenuItem.Owner.Items.Remove(removeMenuItem);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Task.CompletedTask;
        }

        public Task HandleAsync(CategoryAddedMessage message, CancellationToken cancellationToken)
        {
            var menuItem = new ToolStripMenuItem(message.CategoryName);
            _menu.Items.Add(menuItem);
            _categoryMenuItems.Add(string.Empty, menuItem);

            return Task.CompletedTask;
        }
    }
}