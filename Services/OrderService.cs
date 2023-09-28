using ProvaPub.Interfaces;
using ProvaPub.Models;

namespace ProvaPub.Services
{
	public class OrderService
	{
		private readonly Dictionary<string, IPaymentStrategy> _paymentStrategies;
		
		public OrderService(IEnumerable<IPaymentStrategy> paymentStrategies)
		{
			_paymentStrategies = paymentStrategies.ToDictionary(
				strategy => strategy.GetType().Name.ToLower().Replace("paymentstrategy", ""),
				strategy => strategy
			);
		}
		
		public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
			if (!_paymentStrategies.TryGetValue(paymentMethod.ToLower(), out var paymentStrategy))
			{
				throw new InvalidOperationException("Payment method not supported");
			}

			await paymentStrategy.Pay(paymentValue);

			return new Order
			{
				Value = paymentValue
			};
		}
	}
}
