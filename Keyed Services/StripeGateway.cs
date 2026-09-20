namespace DotNet_DependencyInjection_Lab.Keyed_Services
{
    public class StripeGateway : IPaymentGateway
    {
        public string ProcessPayment(decimal amount)
        {
            return $"Stripe : {amount}";
        }
    }
}
