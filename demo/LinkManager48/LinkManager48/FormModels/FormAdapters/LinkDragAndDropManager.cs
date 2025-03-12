using System;
using System.Windows.Forms;
using LinkManager48.Forms;
using LinkManager48.Messages;
using LinkManager48.Models;
using Mffm.Contracts;
using Mffm.Core;

namespace LinkManager48.FormModels.FormAdapters
{
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
            if (!(form is MainForm mainForm)) return;

            var tv = mainForm.Get<TreeView>();
            tv.AllowDrop = true;
            tv.DragEnter += (sender, args) =>
            {
                if (args.Data.GetDataPresent(DataFormats.Text))
                    args.Effect = DragDropEffects.Copy;
            };
            tv.DragDrop += (sender, args) =>
            {
                var n = tv.GetNodeAt(tv.PointToClient(new System.Drawing.Point(args.X, args.Y)));
                var category = n?.Parent?.Text ?? n?.Text ?? string.Empty;

                if (!args.Data.GetDataPresent(DataFormats.Text)) return;
                var link = args.Data.GetData(DataFormats.Text).ToString();

                var linkModel = new MyLink(link, link, category);
                _repository.SaveOrUpdate(linkModel);
                _eventAggregator.Publish(new LinkChangedMessage(linkModel, LinkChangedMessage.TypeOfChange.Created));
            };
        }
    }
}