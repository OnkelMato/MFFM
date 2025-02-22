namespace Mffm.Samples.Core.Logging;

// todo change logging to microsoft logging extension
public interface IBmLogger
{
    // define logging functions
    void LogDebug(string message);
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message, Exception ex);
}