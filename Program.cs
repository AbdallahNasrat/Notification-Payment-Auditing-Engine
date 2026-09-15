namespace DotNet_DependencyInjection_Lab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<IGuidService, TransientGuidService>(); // Different values if is at the last
            builder.Services.AddSingleton<IGuidService, SingletonGuidService>(); //the same values if is at the last even when do new request
            builder.Services.AddScoped<IGuidService, ScopedGuidService>();// the same values if is at the last , because one request , but changed after new request




            builder.Services.AddScoped<AuditService>();


            var app = builder.Build();

            app.MapGet("/testLifeTime", (AuditService auditService ) => {
                var value = auditService._singletonGuid.Value + " Singleton \n " +auditService._scopedGuid.Value + " Scoped \n " + auditService._transientGuid.Value +" Transient";
                return value;
            });


            app.Run();
        }
    }
}
