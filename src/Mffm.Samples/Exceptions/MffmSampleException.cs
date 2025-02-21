namespace Mffm.Samples.Exceptions
{
    /// <summary>
    ///     Application exception to check in try-catch if it is app rethrown exception (already caught)
    /// </summary>
    internal class MffmSampleException : Exception
    {
        public MffmSampleException(string message) : base(message)
        {
        }
    }
}