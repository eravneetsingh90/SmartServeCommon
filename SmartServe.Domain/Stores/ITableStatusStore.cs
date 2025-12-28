using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface ITableStatusStore
	{
		Task<TableStatus> GetTableStatusByCode(string code);
	}
}
