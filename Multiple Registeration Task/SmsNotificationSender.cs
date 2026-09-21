namespace DotNet_DependencyInjection_Lab.Multiple_Registeration_Task
{
    public class SmsNotificationSender : INotificationSender
    {
        public string Send(string msg)
        {
            return $"Sms : {msg}";
        }       
    }
}
