namespace DotNet_DependencyInjection_Lab.Multiple_Registeration_Task
{
    public class EmailNotificationSender : INotificationSender
    {
        public string Send(string msg)
        {
            return $"Email : {msg}";
        }
    }
}
