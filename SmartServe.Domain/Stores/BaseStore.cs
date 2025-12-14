using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;

namespace SmartServe.Domain.Stores
{
	public class BaseStore<T> where T : class
	{
		protected readonly SmartServeDbContext _db;
		protected readonly DbSet<T> _set;

		public BaseStore(SmartServeDbContext db)
		{
			_db = db;
			_set = _db.Set<T>();
		}

		// READ: Get all
		public virtual async Task<List<T>> GetAllAsync(
			bool asNoTracking = true)
		{
			return asNoTracking
				? await _set.AsNoTracking().ToListAsync()
				: await _set.ToListAsync();
		}

		// READ: Get by primary key
		public virtual async Task<T?> GetByIdAsync(
			object id,
			bool asNoTracking = false)
		{
			if (asNoTracking)
			{
				return await _set
					.AsNoTracking()
					.FirstOrDefaultAsync(e =>
						EF.Property<object>(e, "Id").Equals(id));
			}

			return await _set.FindAsync(id);
		}

		// CREATE
		public virtual async Task AddAsync(T entity)
		{
			_set.Add(entity);
			await _db.SaveChangesAsync();
		}

		// UPDATE
		public virtual async Task UpdateAsync(T entity)
		{
			_set.Update(entity);
			await _db.SaveChangesAsync();
		}

		// DELETE
		public virtual async Task DeleteAsync(T entity)
		{
			_set.Remove(entity);
			await _db.SaveChangesAsync();
		}

		// SAVE (for batch operations)
		public virtual async Task SaveChangesAsync()
		{
			await _db.SaveChangesAsync();
		}
	}
}
