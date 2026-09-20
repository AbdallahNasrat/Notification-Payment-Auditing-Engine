
namespace DotNet_DependencyInjection_Lab.Keyed_Services
{
    public class PayPalGateway : IPaymentGateway
    {
        public string ProcessPayment(decimal amount)
        {
            return $"PayPal : {amount}";

        }
    }
}
