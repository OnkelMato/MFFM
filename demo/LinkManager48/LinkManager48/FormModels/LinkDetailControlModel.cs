using System.ComponentModel;
using System.Windows.Forms;
using LinkManager48.Models;
using Mffm.Contracts;

namespace LinkManager48.FormModels
{
    internal class LinkDetailControlModel : IExtendedFormModel<MyLink>, INotifyPropertyChanged
    {
        public LinkDetailControlModel()
        {
        }

        private string _linkUrl;
        private string _linkTitle;
        private string _linkCategory;
        private MyLink _context;

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
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
    }
}
