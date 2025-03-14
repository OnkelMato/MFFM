using MediatR;

namespace WinFormsMffmPrototype.Ui.Protocol;

public class LogMessage : INotification
{
    public string Message { get; set; }

    public DateTime Timestamp { get; set; }
}

public class ErrorMessage : INotification
{
    public string Error { get; set; }

    public DateTime Timestamp { get; set; }
}