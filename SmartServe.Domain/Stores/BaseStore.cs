using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Db;

namespace SmartServe.Domain.Stores
{
	public abstract class BaseStore<T> : IBaseStore<T> where T : class
	{
		protected readonly SmartServeDbContext Db;
		protected readonly DbSet<T> Set;

		protected BaseStore(SmartServeDbContext db)
		{
			Db = db;
			Set = db.Set<T>();
		}

		// ================= READ =================

		public virtual async Task<List<T>> GetAllAsync()
		{
			return await Set.AsNoTracking().ToListAsync();
		}

		public virtual async Task<T?> GetByIdAsync<TKey>(TKey id)
		{
			return await Set.AsNoTracking()
				.FirstOrDefaultAsync(e =>
					EF.Property<TKey>(e, "Id")!.Equals(id));
		}

		public virtual void Add(T entity)
		{
			Set.Add(entity);
		}
		public virtual void AddRange(List<T> entities)
		{
			Set.AddRange(entities);
		}
		public virtual void RemoveRange(List<T> entities)
		{
			Set.RemoveRange(entities);
		}
		public virtual void Attach(T entity)
		{
			var entry = Db.Entry(entity);
			if (entry.State == EntityState.Detached)
			{
				Set.Attach(entity);
			}
		}

		public virtual void Remove(T entity)
		{
			var entry = Db.Entry(entity);

			if (entry.State == EntityState.Detached)
			{
				// Reuse tracked instance if present
				var tracked = Db.ChangeTracker
					.Entries<T>()
					.FirstOrDefault(e =>
						EF.Property<object>(e.Entity, "Id")!
							.Equals(EF.Property<object>(entity, "Id")));

				entity = tracked?.Entity ?? entity;
				if (tracked == null)
					Set.Attach(entity);
			}

			Set.Remove(entity);
		}

		// ================= COMMIT =================

		public virtual Task SaveAsync()
		{
			return Db.SaveChangesAsync();
		}
	}
}
