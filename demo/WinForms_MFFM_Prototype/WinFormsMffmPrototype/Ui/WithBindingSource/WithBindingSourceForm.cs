namespace WinFormsMffmPrototype.Ui.WithBindingSource
{
    public partial class WithBindingSourceForm : Form
    {
        public WithBindingSourceForm()
        {
            InitializeComponent();

            this.bindingSource1.DataSource = new WithBindingSourceFormModel()
            {
            };
        }
    }
}
