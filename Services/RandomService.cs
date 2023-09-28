namespace ProvaPub.Services
{
	public class RandomService
	{
		private readonly Random _random;
    
		public RandomService()
		{
			var seed = Guid.NewGuid().GetHashCode();
			_random = new Random(seed);
		}
    
		public int GetRandom()
		{
			return _random.Next(100);
		}
	}
}
