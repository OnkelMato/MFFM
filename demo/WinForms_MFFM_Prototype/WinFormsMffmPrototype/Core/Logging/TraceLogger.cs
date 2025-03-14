using System.Diagnostics;

namespace WinFormsMffmPrototype.Core.Logging
{
    internal class TraceLogger : IMffmLogger
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
}
