using System.Diagnostics;

namespace Mffm.Samples.Core.Logging;

internal class TraceLogger : IBmLogger
{
    public void LogDebug(string message)
    {
        Trace.WriteLine($"DEBUG: {message}");
    }

    public void LogInfo(string message)
    {
        Trace.WriteLine($"INFO: {message}");
    }

    public void LogWarning(string message)
    {
        Trace.WriteLine($"WARNING: {message}");
    }

    public void LogError(string message, Exception ex)
    {
        Trace.WriteLine($"ERROR: {message} - Exception: {ex}");
    }
}