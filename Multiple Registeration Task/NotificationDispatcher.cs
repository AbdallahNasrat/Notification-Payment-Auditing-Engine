using System.Security.Cryptography.X509Certificates;

namespace DotNet_DependencyInjection_Lab.Multiple_Registeration_Task
{
    public class NotificationDispatcher
    {
        IEnumerable<INotificationSender> _senders;
        List<string> Results = new List<string>();
        public NotificationDispatcher(IEnumerable<INotificationSender> senders)
        {
            _senders = senders;
        }

        public List<string>Dispatch(string msg) {


            foreach (var sender in _senders) {
                Results.Add(sender.Send(msg));
            }
            return Results;
        }
    }
}
