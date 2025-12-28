using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface ITableStatusStore : IBaseStore<TableStatus>
	{
		Task<TableStatus> GetTableStatusByCode(string code);
	}
}
