namespace WinFormsMffmPrototype.Core.Services;

public interface IGreetingRepository
{
    string GetGreeting(IDateTimeProvider dateTime);

}