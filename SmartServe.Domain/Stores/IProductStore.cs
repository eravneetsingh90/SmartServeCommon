using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IProductStore : IBaseStore<Product>
	{
		Task<List<Product>> GetByCategoryIdAsync(int categoryId);
		Task SaveBulkAsync(IEnumerable<Product> products);
		Task DeleteAsync(int id);
	}
}
