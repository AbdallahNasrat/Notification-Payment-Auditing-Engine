namespace DotNet_DependencyInjection_Lab.Keyed_Services
{
    public interface IPaymentGateway
    {
        public string ProcessPayment(decimal amount);
    }
}
