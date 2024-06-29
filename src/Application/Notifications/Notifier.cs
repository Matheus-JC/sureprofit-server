namespace SureProfit.Application.Notifications;

public class Notifier : INotifier
{
    private readonly List<Notification> _notifications = [];

    public bool HasNotification()
    {
        return _notifications.Count != 0;
    }

    public List<Notification> GetNotifications()
    {
        return _notifications;
    }

    public void Handle(Notification notification)
    {
        _notifications.Add(notification);
    }

    public void Handle(IEnumerable<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }
}
