using SmartServe.Domain.Models;

namespace SmartServe.Domain.Stores
{
	public interface IRestaurantTableStore
	{
		Task CreateTableAsync(string displayName);
		Task SoftDeleteTableAsync(int tableId);
		Task<List<GetTableView>> GetTablesForViewAsync();
	}
}
