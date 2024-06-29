namespace SureProfit.Application.Notifications;

public interface INotifier
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notification);
    void Handle(IEnumerable<Notification> notifications);
}
