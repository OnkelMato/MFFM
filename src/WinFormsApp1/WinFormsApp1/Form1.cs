using System.Windows.Input;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            bindingSource1.DataSource = new Form1Model();

        }
    }

    public class Form1Model
    {
        public string Name { get; set; } = "Thomas";

        public ICommand SoSomething { get; set; }
    }
}
