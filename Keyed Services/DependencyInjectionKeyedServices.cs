
using DotNet_DependencyInjection_Lab.Keyed_Services;
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionKeyedServices
    {
        public static IServiceCollection AddPaymentServices(this IServiceCollection services) {
            services.AddKeyedScoped<IPaymentGateway, StripeGateway>("stripe");
            services.AddKeyedScoped<IPaymentGateway, PayPalGateway>("paypal");

            services.AddScoped<IPaymentGateway>(sp => {
                var paymentProvider = sp.GetRequiredService<IConfiguration>().GetValue<string>("DefaultPaymentProvider");
                if (paymentProvider == "stripe")
                    return sp.GetKeyedService<IPaymentGateway>("stripe");
                else
                    return sp.GetKeyedService<IPaymentGateway>("paypal");
            }
            );

            return services;
        }
    }
}
