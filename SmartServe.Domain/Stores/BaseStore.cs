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

		// ================= READ =================

		public virtual async Task<List<T>> GetAllAsync()
		{
			// 🔑 ALWAYS DETACHED
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

		public virtual async Task AddAsync(T entity)
		{
			_set.Add(entity);
			await _db.SaveChangesAsync();
		}

		// ================= UPDATE =================

		public virtual async Task UpdateAsync(T entity)
		{
			// 🔑 PREVENT MULTIPLE TRACKING
			_db.ChangeTracker.Clear();

			_set.Attach(entity);
			_db.Entry(entity).State = EntityState.Modified;

			await _db.SaveChangesAsync();
		}

		// ================= DELETE =================

		public virtual async Task DeleteAsync(T entity)
		{
			_db.ChangeTracker.Clear();

			_set.Attach(entity);
			_set.Remove(entity);

			await _db.SaveChangesAsync();
		}
	}
}
