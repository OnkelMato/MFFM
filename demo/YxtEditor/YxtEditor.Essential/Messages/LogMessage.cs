namespace YxtEditor.Essential.Messages;

internal class LogMessage(string message)
{
    public string Message { get; set; } = message;
}