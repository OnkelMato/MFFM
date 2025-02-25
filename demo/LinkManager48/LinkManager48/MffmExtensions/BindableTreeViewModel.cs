using System.ComponentModel;

namespace LinkManager48.MffmExtensions
{
    internal class BindableTreeViewModel
    {
        public string Text { get; set; }
        public BindingList<BindableTreeViewModel> Children { get; set; }
    }
}