namespace Mffm.Samples.Core.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    //public DateTime Now { get; } = DateTime.Now;
}