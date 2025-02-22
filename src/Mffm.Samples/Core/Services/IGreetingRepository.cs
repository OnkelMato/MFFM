namespace Mffm.Samples.Core.Services;

public interface IGreetingRepository
{
    string GetGreeting(IDateTimeProvider dateTime);
}