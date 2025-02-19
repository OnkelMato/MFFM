using System.Windows.Forms;
using System.Windows.Input;

namespace SampleUI
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();

            MagicCommand = new MessageBoxCommand();

            bindableButton1.DataBindings.Add(
                new Binding("CommandParameter", this, null, true));
            bindableButton1.DataBindings.Add(
                new Binding(nameof(bindableButton1.Command), this, nameof(MagicCommand), true));
        }

        public ICommand MagicCommand { get; set; }
    }
}
