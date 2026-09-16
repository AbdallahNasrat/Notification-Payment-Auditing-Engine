using DotNet_DependencyInjection_Lab.Multiple_Registeration_Task;
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionNotification
    {
        public static IServiceCollection NotificationServices(this IServiceCollection services) {
            services.AddTransient<INotificationSender, SmsNotificationSender>();
            services.AddTransient<INotificationSender, EmailNotificationSender>();
            services.AddScoped<NotificationDispatcher>();
            return services;
        }
    }
}
