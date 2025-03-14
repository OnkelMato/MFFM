using WinFormsMffmPrototype.MvvmFramework;
using WinFormsMffmPrototype.MvvmFramework.EventAggregators;
using WinFormsMffmPrototype.Ui.EditUser;
using WinFormsMffmPrototype.Ui.Protocol;

namespace WinFormsMffmPrototype.Ui.Main
{
    public partial class MainForm : Form, IHandle<LogMessage>
    {
        [Obsolete("only for design time")]
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(
            MainFormModel mainFormModel, 
            IWindowManager windowManager,
            IEventAggregator eventAggregator) : this()
        {

            eventAggregator.Subscribe(this);

            helloRalfButton.Command = new FormModelAsParameterCommandDecorator(new HelloRalfCommand(), mainFormModel);

            editUserButton.Command = new RelayCommand((ctx) =>
            {
                windowManager.Show<EditForm>();
            }, (ctx) => true);
            editUserButton.CommandParameter = mainFormModel; // so the context is set properly

            textBox1.DataBindings.Add(
                nameof(textBox1.Text),
                mainFormModel,
                nameof(mainFormModel.Name),
                true,
                DataSourceUpdateMode.OnPropertyChanged);

            button2.Command = new FormModelAsParameterCommandDecorator(new RelayCommand(
                mainFormModel.DoSomethingElse,
                mainFormModel.CanDoSomethingElse), mainFormModel);


            // Verbinden der UI mit dem Formularmodel
            button1.Click += (sender, args) => mainFormModel.Save();
            button3.Command = new RelayCommand(mainFormModel.OpenProtocolForm, (ctx) => true);
            textBox2.DataBindings.Add("Text", mainFormModel, nameof(mainFormModel.Name), true, DataSourceUpdateMode.OnPropertyChanged);
        }

        public Task HandleAsync(LogMessage message, CancellationToken cancellationToken)
        {
            this.label1.Text = message.Message;
            return Task.CompletedTask;
        }
    }
}
