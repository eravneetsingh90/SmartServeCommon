using SmartServe.Domain.Models;
using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IRestaurantTableStore : IBaseStore<RestaurantTable>
	{
		Task CreateTableAsync(string displayName);
		Task<List<GetTableView>> GetTablesForViewAsync();
	}
}
