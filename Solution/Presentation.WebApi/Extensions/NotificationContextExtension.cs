using Infra.Notification.Contexts;

namespace Presentation.WebApi.Extensions
{
    public static class NotificationContextExtension
    {
        public static void AddNotificationContextExtension(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<NotificationContext>();
        }
    }
}