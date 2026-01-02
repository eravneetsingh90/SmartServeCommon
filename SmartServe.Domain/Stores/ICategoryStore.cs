using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface ICategoryStore : IBaseStore<Category>
	{
		Task<List<Category>> GetActiveAsync();
		Task<List<Category>> GetAllAsync();
		Task SaveBulkAsync(IEnumerable<Category> categories);
		Task DeleteAsync(int id);
	}
}
