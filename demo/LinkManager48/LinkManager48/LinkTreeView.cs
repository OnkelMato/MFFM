using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinkManager48.MffmExtensions;
using LinkManager48.Models;
using Mffm.Contracts;
using Mffm.Core;

namespace LinkManager48
{
    internal partial class LinkTreeView : UserControl, IHandle<LinkChangedMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILinkFactory _linkFactory;

        public LinkTreeView()
        {
            InitializeComponent();

            // get services from IoC container. As we cannot use DI here, we need to that this way
            _eventAggregator = Program.GetService<IEventAggregator>();
            _eventAggregator?.Subscribe(this);

            _linkFactory = Program.GetService<ILinkFactory>() ?? new LinkFactory(new DefaultHttpClient());

            treeView.RootModels = new BindingList<BindableTreeViewModel>((new[] { new BindableTreeViewModel() { Text = "Test" } }).ToList());
        }

        public Task HandleAsync(LinkChangedMessage message, CancellationToken cancellationToken)
        {


            return Task.CompletedTask;
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            var data = (string)e.Data.GetData(typeof(string));
            if (!string.IsNullOrEmpty(data))
            {
                var title = new Uri(data).Host;
                try
                {
                    title = _linkFactory.Create(data).Title;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }

                var msg = new LinkChangedMessage(
                    new MyLink(data, title),
                    ChangeType.Created);
                _eventAggregator.Publish(msg);
            }
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }
    }
}
