namespace WinFormsMffmPrototype.Exceptions;

/// <summary>
/// Application exception to check in try-catch if it is app rethrown exception (already caught)
/// </summary>
internal class MyAppException : Exception
{
    public MyAppException(string message) : base(message) { }
}