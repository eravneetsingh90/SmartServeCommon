using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Db;
using SmartServe.Resources.Provider;
using System.Linq.Expressions;

namespace SmartServe.Domain.Stores
{
    public abstract class BaseStore<T> : IBaseStore<T> where T : class
    {
        protected readonly ITenantProvider TenantProvider;

        protected readonly SmartServeDbContext Db;
        protected readonly DbSet<T> Set;

        protected BaseStore(SmartServeDbContext db, ITenantProvider tenantProvider)
        {
            Db = db;
            Set = db.Set<T>();
            TenantProvider = tenantProvider;
        }

        // ================= READ =================

        public virtual Task<List<T>> GetAllAsync()
        {
            return Set.AsNoTracking()
                .Where(e => EF.Property<int>(e, "TenantId")!.Equals(TenantProvider.TenantId))
                .ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync<TKey>(TKey id)
        {
            return await Set.AsNoTracking()
        .FirstOrDefaultAsync(e =>
            EF.Property<TKey>(e, "Id")!.Equals(id) &&
            EF.Property<TKey>(e, "TenantId")!.Equals(TenantProvider.TenantId));
        }
        public virtual void Add(T entity)
        {
            Set.Add(entity);
        }
        public virtual void AddRange(List<T> entities)
        {
            Set.AddRange(entities);
        }
        public virtual void Update(T entity)
        {
            Set.Update(entity);
        }
        public virtual void UpdateRange(List<T> entities)
        {
            Set.UpdateRange(entities);
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
            //var entry = Db.Entry(entity);

            //if (entry.State == EntityState.Detached)
            //{
            //    // Reuse tracked instance if present
            //    var tracked = Db.ChangeTracker
            //        .Entries<T>()
            //        .FirstOrDefault(e =>
            //            EF.Property<object>(e.Entity, "Id")!
            //                .Equals(EF.Property<object>(entity, "Id")));

            //    entity = tracked?.Entity ?? entity;
            //    if (tracked == null)
            //        Set.Attach(entity);
            //}

            Set.Remove(entity);
        }

        // ================= COMMIT =================

        public virtual Task SaveAsync()
        {
            return Db.SaveChangesAsync();
        }
    }
}
