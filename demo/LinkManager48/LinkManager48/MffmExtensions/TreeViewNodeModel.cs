using System.ComponentModel;

namespace LinkManager48.MffmExtensions
{
    internal class TreeViewNodeModel
    {
        public virtual string Text { get; set; }
        public virtual BindingList<TreeViewNodeModel> Children { get; set; } = new BindingList<TreeViewNodeModel>();

        public object Data { get; set; }
    }
}