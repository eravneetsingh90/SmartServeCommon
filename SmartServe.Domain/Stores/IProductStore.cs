using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IProductStore : IBaseStore<Product>
	{
		Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
		Task SaveBulkProductsAsync(IEnumerable<Product> products);
	}
}
