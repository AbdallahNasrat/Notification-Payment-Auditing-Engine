namespace DotNet_DependencyInjection_Lab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Grouping Services (Service Registration Extensions
            builder.Services.AddDIServices();




            
            var app = builder.Build();

            app.MapGet("/testLifeTime", (AuditService auditService ) => {
                var value = auditService._singletonGuid.Value + " Singleton \n " +auditService._scopedGuid.Value + " Scoped \n " + auditService._transientGuid.Value +" Transient";
                return value;
            });


            app.Run();
        }
    }
}
