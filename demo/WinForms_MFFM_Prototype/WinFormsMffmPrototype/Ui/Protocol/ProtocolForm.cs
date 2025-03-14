using WinFormsMffmPrototype.MvvmFramework;

namespace WinFormsMffmPrototype.Ui.Protocol
{
    public partial class ProtocolForm : Form, IWindow
    {
        private readonly ProtocolFormModel _model;

        [Obsolete("only for design time")]
        public ProtocolForm()
        {
            InitializeComponent();
        }

        public ProtocolForm(ProtocolFormModel model) : this()
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            //_model.PropertyChanged += (sender, args) => textBox1.Text = _model.Log;

            button1.Command = new FormModelAsParameterCommandDecorator( model.CloseWindow, model);

            // Verbinden der UI mit dem Formularmodel
            //textBox1.DataContext = _model;
            textBox1.DataBindings.Add(nameof(textBox1.Text), _model, nameof(_model.Log), true, DataSourceUpdateMode.OnPropertyChanged);
        }

        public object FormModel => _model;
    }

}
