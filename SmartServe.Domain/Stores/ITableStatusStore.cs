using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface ITableStatusStore : IBaseStore<TableStatusEntity>
	{
		Task<TableStatusEntity> GetTableStatusByCode(string code);
	}
}
