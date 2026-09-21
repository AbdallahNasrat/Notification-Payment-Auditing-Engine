using DotNet_DependencyInjection_Lab;
using DotNet_DependencyInjection_Lab.Database_initializerTask;
using DotNet_DependencyInjection_Lab.Keyed_Services;
using DotNet_DependencyInjection_Lab.Multiple_Registeration_Task;
using Microsoft.Extensions.Configuration;

namespace DotNet_DependencyInjection_Lab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Grouping Services (Service Registration Extensions
            builder.Services.AddDIServices();

            //Multiple Registrations
            builder.Services.NotificationServices();

            //Keyed Services & Factory Installation
            builder.Services.AddPaymentServices();

            //DatabaseInit Task
            builder.Services.AddSingleton<DatabaseInitializer>();
            //builder.Services.AddSingleton(typeof(DatabaseInitializer)); another way





            
            var app = builder.Build();

            app.MapGet("/testLifeTime", (AuditService auditService ) => {
                var value = auditService._singletonGuid.Value + " Singleton \n " +auditService._scopedGuid.Value + " Scoped \n " + auditService._transientGuid.Value +" Transient";
                return value;
            });

            app.MapGet("/MultipleRegistrations/{msg}", (string msg, NotificationDispatcher dispatcher) =>
            {
                return dispatcher.Dispatch(msg);
            });

            app.MapGet("/keyedServices/{amount}", (decimal amount, IPaymentGateway paymentGateway) =>
            {
                return paymentGateway.ProcessPayment(amount);
            });


            //DatabaseInit Task - Create Scope
            using (var scope = app.Services.CreateScope())
            {
                var init = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
                init.Initializer();
            }
            app.Run();  
        }
    }
}
