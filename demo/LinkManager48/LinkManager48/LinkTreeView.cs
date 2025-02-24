using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mffm.Contracts;
using Mffm.Core;

namespace LinkManager48
{
    internal partial class LinkTreeView : UserControl, IHandle<LinkChangedMessage>
    {
        private IEventAggregator _eventAggregator;

        public LinkTreeView()
        {
            InitializeComponent();

            
            _eventAggregator = Program.GetService<IEventAggregator>();
            if (_eventAggregator != null)
                _eventAggregator.Subscribe(this);
        }

        public void InitializeDi(IEventAggregator eventAggregator)
        {
        }

        public Task HandleAsync(LinkChangedMessage message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            // convert to bus message to create item
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);
                // Do whatever you need to do with clipboardText
                var msg = new LinkChangedMessage(
                    new MyLink(clipboardText),
                    ChangeType.Created);
                _eventAggregator.Publish(msg);
            }
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }
    }

    internal class MyLink
    {
        public string Link { get; }

        public MyLink(string link)
        {
            Link = link;
        }
    }

    internal class LinkChangedMessage
    {
        public LinkChangedMessage(MyLink link, ChangeType changeType)
        {
            Link = link;
            ChangeType = changeType;
        }

        public MyLink Link { get; }
        public ChangeType ChangeType { get; }
    }

    internal enum ChangeType
    {
        Created = 1,
        Changed = 2,
        Deleted = 3
    }
}
