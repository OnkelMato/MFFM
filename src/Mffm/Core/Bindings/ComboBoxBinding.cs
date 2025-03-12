using Mffm.Contracts;

namespace Mffm.Core.Bindings
{
    internal class ComboBoxBinding : IControlBinding
    {
        private const string Items = "Items";

        public bool Bind(Control control, IFormModel formModel)
        {
            // check if control and properties exist
            if (!(control is ComboBox comboBox)) return false;
            var propertyText = formModel.GetType().GetProperty(comboBox.Name);
            var propertyList = formModel.GetType().GetProperty(comboBox.Name + Items);
            if (propertyText == null || propertyList == null) return true; // we cannot bind so this control is not bindable. We can stop here.

            // bind datasource and selected item
            if (propertyList.GetValue(formModel) is IEnumerable<object> list)
                if (list.Contains(null))
                    throw new ArgumentException("List cannot contain null values");

            comboBox.DataBindings.Add(nameof(comboBox.DataSource), formModel, propertyList.Name, true, DataSourceUpdateMode.OnPropertyChanged);
            comboBox.DataBindings.Add(nameof(comboBox.SelectedItem), formModel, propertyText.Name, true, DataSourceUpdateMode.OnPropertyChanged);
            return true;
        }
    }
}