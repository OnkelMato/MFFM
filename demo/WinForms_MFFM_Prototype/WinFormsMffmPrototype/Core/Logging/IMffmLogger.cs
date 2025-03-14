namespace WinFormsMffmPrototype.Core.Logging;

public interface IMffmLogger
{
    // define logging functions
    void LogDebug(string message);
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message, Exception ex);
}