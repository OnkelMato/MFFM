using System.Windows.Forms;
using Mffm.Contracts;

namespace LinkManager48.MffmExtensions
{
    internal class BindableTreeViewBinding : IControlBinding
    {
        public bool Bind(Control control, IFormModel formModel)
        {
            if (!(control is BindableTreeView treeView)) return false;

            if (formModel.GetType().GetProperty(treeView.Name) != null)
                treeView.DataBindings.Add(new Binding(nameof(BindableTreeView.RootModels), formModel, treeView.Name));

            if (formModel.GetType().GetProperty(treeView.Name + "Selected") != null)
                treeView.DataBindings.Add(new Binding(nameof(BindableTreeView.SelectedModel), formModel, treeView.Name + "Selected", true, DataSourceUpdateMode.OnPropertyChanged));

            return true;
        }
    }
}