namespace Mffm.Samples.Core.Logging;

internal class EmptyLogger : IBmLogger
{
    public void LogDebug(string message)
    {
        // Do nothing
    }

    public void LogInfo(string message)
    {
        // Do nothing
    }

    public void LogWarning(string message)
    {
        // Do nothing
    }

    public void LogError(string message, Exception ex)
    {
        // Do nothing
    }
}