namespace Mffm.Samples.Core.Services
{
    public class GreetingRepository : IGreetingRepository
    {
        public string GetGreeting(IDateTimeProvider dateTime)
        {
            return dateTime.Now.Hour switch
            {
                < 12 => "Good Morning",
                < 17 => "Good Afternoon",
                _ => "Good Evening"
            };
        }
    }
}