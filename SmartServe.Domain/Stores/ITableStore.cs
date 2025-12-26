namespace SmartServe.Domain.Stores
{
	public interface ITableStore
	{
		Task UpdateTableStatusAsync(int tableId, int statusId);
	}

}
