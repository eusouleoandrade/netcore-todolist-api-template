using Infra.Notification.Abstractions;

namespace Infra.Notification.Contexts
{
    public class NotificationContext : Notifiable
    {
    }

    public class NotificationContext<TNotificationMessage> : Notifiable<TNotificationMessage>
        where TNotificationMessage : class
    {
    }
}
