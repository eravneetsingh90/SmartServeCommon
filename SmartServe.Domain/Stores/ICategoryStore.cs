using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface ICategoryStore : IBaseStore<CategoryEntity>
	{
		Task<List<CategoryEntity>> GetActiveAsync();
		Task<List<CategoryEntity>> GetAllAsync();
		Task SaveBulkAsync(IEnumerable<CategoryEntity> categories);
		Task DeleteAsync(int id);
	}
}
