using System;
using System.Windows.Forms;
using LinkManager48.Models;
using Mffm.Contracts;

namespace LinkManager48.FormModels
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