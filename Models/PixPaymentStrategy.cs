using ProvaPub.Interfaces;

namespace ProvaPub.Models;

public class PixPaymentStrategy : IPaymentStrategy
{
    public Task Pay(decimal paymentValue)
    {
        Console.WriteLine("Faz pagamento via Pix...");
        return Task.CompletedTask;
    }
}