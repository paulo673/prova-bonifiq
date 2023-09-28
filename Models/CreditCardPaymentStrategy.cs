using ProvaPub.Interfaces;

namespace ProvaPub.Models;

public class CreditCardPaymentStrategy : IPaymentStrategy
{
    public Task Pay(decimal paymentValue)
    {
        Console.WriteLine("Faz pagamento via cartão de crédito...");
        return Task.CompletedTask;
    }
}