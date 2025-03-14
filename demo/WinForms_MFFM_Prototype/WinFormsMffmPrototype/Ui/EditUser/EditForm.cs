using WinFormsMffmPrototype.MvvmFramework;

namespace WinFormsMffmPrototype.Ui.EditUser
{
    /// <summary>
    /// Edit form which uses the BindingSource for data binding
    /// </summary>
    public partial class EditForm : Form, IWindow
    {
        private readonly EditFormModel _formModel;

        [Obsolete("only for design time")]
        public EditForm()
        {
            InitializeComponent();
        }

        public EditForm(EditFormModel formModel)
        : this()
        {
            this.bindingSource.DataSource = formModel;
            _formModel = formModel ?? throw new ArgumentNullException(nameof(formModel));
        }

        public object FormModel => _formModel;
    }
}
