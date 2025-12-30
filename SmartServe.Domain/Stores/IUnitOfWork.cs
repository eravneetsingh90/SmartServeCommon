namespace SmartServe.Domain.Stores
{
	public interface IUnitOfWork
	{
		Task BeginAsync();
		Task CommitAsync();
		Task RollbackAsync();
	}

}
