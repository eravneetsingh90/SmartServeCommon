using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Db;

namespace SmartServe.Domain.Stores
{
	public class BaseStore<T> : IBaseStore<T> where T : class
	{
		protected readonly SmartServeDbContext _db;
		protected readonly DbSet<T> _set;

		public BaseStore(SmartServeDbContext db)
		{
			_db = db;
			_set = _db.Set<T>();
		}

		// ================= READ =================

		public virtual async Task<List<T>> GetAllAsync()
		{
			return await _set.AsNoTracking().ToListAsync();
		}

		public virtual async Task<T?> GetByIdAsync(object id)
		{
			return await _set
				.AsNoTracking()
				.FirstOrDefaultAsync(e =>
					EF.Property<object>(e, "Id")!.Equals(id));
		}

		// ================= CREATE =================

		public virtual Task AddAsync(T entity)
		{
			_set.Add(entity);
			return Task.CompletedTask;
		}

		public virtual async Task AddAndSaveAsync(T entity)
		{
			_set.Add(entity);
			await _db.SaveChangesAsync();
		}
		// ================= UPDATE =================

		public virtual Task UpdateAsync(T entity)
		{
			_db.ChangeTracker.Clear();

			_set.Attach(entity);
			_db.Entry(entity).State = EntityState.Modified;

			return Task.CompletedTask;
		}

		public virtual async Task UpdateAndSaveAsync(T entity)
		{
			_db.ChangeTracker.Clear();

			_set.Attach(entity);
			_db.Entry(entity).State = EntityState.Modified;

			await _db.SaveChangesAsync();

		}
		// ================= DELETE =================

		public virtual Task DeleteAsync(T entity)
		{
			_db.ChangeTracker.Clear();

			_set.Attach(entity);
			_set.Remove(entity);

			return Task.CompletedTask;
		}
		public virtual async Task DeleteAndSaveAsync(T entity)
		{
			_db.ChangeTracker.Clear();

			_set.Attach(entity);
			_set.Remove(entity);

			await _db.SaveChangesAsync();
		}
	}
}
