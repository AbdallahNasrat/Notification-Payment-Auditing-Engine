using DotNet_DependencyInjection_Lab;
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services ) {
            services.AddTransient<IGuidService, TransientGuidService>(); // Different values if is at the last
            services.AddSingleton<IGuidService, SingletonGuidService>(); //the same values if is at the last even when do new request
            services.AddScoped<IGuidService, ScopedGuidService>();// the same values if is at the last , because one request , but changed after new request

            services.AddScoped<AuditService>();
            return services;

        }
    }
}
