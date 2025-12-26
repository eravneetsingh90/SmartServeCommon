namespace SmartServe.Domain.Stores
{
	namespace SmartServe.Domain.Stores
	{
		public interface IBaseStore<T> where T : class
		{
			Task<List<T>> GetAllAsync();
			Task<T?> GetByIdAsync(object id);
			Task AddAsync(T entity);
			Task AddAndSaveAsync(T entity);
			Task UpdateAsync(T entity);
			Task UpdateAndSaveAsync(T entity);
			Task DeleteAsync(T entity);
			Task DeleteAndSaveAsync(T entity);
		}
	}

}
