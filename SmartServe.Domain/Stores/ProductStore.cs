using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class ProductStore : BaseStore<Product>
	{
		public ProductStore(SmartServeDbContext db) : base(db) { }

		public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
		{
			return await _db.Products
				.Where(p => p.IsActive == true && p.CategoryId == categoryId)
				.OrderBy(p => p.Name)
				.ToListAsync();
		}
	}

}
