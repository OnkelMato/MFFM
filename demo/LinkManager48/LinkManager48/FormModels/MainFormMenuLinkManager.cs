using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mffm.Contracts;

namespace LinkManager48.FormModels
{
    internal class MainFormMenuLinkManager : IFormAdapter, IHandle<LinkChangedMessage>
    {
        private ToolStripMenuItem _menu;

        public MainFormMenuLinkManager(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public void InitializeWith(Form form)
        {
            _menu = form.MainMenuStrip.Items.Find("linksToolStripMenuItem", true).FirstOrDefault() as ToolStripMenuItem;
        }

        public Task HandleAsync(LinkChangedMessage message, CancellationToken cancellationToken)
        {
            if (message.ChangeType == ChangeType.Created)
            {
                var item = new ToolStripMenuItem(message.Link.Title);
                item.Click += (sender, args) => System.Diagnostics.Process.Start(message.Link.Link);
                _menu?.DropDownItems.Add(item);
            }

            return Task.CompletedTask;
        }
    }
}