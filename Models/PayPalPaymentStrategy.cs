using ProvaPub.Interfaces;

namespace ProvaPub.Models;

public class PayPalPaymentStrategy : IPaymentStrategy
{
    public Task Pay(decimal paymentValue)
    {
        Console.WriteLine("Faz pagamento via PayPal...");
        return Task.CompletedTask;
    }
}