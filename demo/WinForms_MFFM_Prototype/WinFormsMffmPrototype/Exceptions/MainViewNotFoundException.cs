namespace WinFormsMffmPrototype.Exceptions;

internal class MainViewNotFoundException : MyAppException
{
    private const string Message = "Main view konnte nicht gefunden werden. Bitte DI checken";
    public MainViewNotFoundException()
        : base(Message) { }
}