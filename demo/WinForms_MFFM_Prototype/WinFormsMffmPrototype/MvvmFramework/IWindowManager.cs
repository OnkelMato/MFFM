namespace WinFormsMffmPrototype.MvvmFramework;

public interface IWindowManager
{
    void Show<TForm>() where TForm : Form, IWindow;
    bool IsFormOpen(object parameter);
    void CloseForm(object parameter);
}