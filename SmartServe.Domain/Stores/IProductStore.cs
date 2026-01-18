using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IProductStore : IBaseStore<ProductEntity>
	{
		Task<List<ProductEntity>> GetByCategoryIdAsync(int categoryId);
		Task SaveBulkAsync(IEnumerable<ProductEntity> products);
		Task DeleteAsync(int id);
	}
}
