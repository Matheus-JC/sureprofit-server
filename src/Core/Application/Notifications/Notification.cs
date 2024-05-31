namespace SureProfit.Application.Notifications
{
    public class Notification(string message)
    {
        public string Message { get; } = message;
    }
}
