using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface ICategoryStore : IBaseStore<Category>
	{
		Task<List<Category>> GetActiveCategoriesAsync();
		Task<List<Category>> GetAllCategoriesByOrderAsync();
		Task SaveBulkCategoriesAsync(IEnumerable<Category> categories);		
	}
}
