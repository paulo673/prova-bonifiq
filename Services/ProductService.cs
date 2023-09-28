using ProvaPub.Models;
using ProvaPub.QueryableExtensions;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService
	{
		private readonly TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public async Task<PagedList<Product>> ListProducts(int page)
		{
			return await _ctx.Products.GetPagedList(page);
		}
	}
}
