using SmartServe.EFCore.Db;
using SmartServe.EFCore.Entities;

namespace SmartServe.Domain.Stores
{
	public class ProductStore : BaseStore<Product>
	{
		public ProductStore(SmartServeDbContext db)
			: base(db)
		{
		}

		// Get only active products
		//public async Task<List<Product>> GetActiveProductsAsync()
		//{
		//	return await _set
		//		.AsNoTracking()
		//		.Where(p => p.is_active == true)
		//		.OrderBy(p => p.name)
		//		.ToListAsync();
		//}

		//// Get product with relations (for POS screen)
		//public async Task<Product?> GetProductWithDetailsAsync(int productId)
		//{
		//	return await _set
		//		.Include(p => p.category)
		//		.Include(p => p.brand)
		//		.Include(p => p.flavor)
		//		.Include(p => p.serving_type)
		//		.FirstOrDefaultAsync(p => p.product_id == productId);
		//}
	}
}
