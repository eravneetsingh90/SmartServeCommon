using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
    public class TenantStore : BaseStore<TenantEntity>, ITenantStore
    {
        public TenantStore(SmartServeDbContext db) : base(db) { }

        public async Task<TenantEntity?> GetByDomainAsync(string tenantName)
        {
            return await Set
                .AsNoTracking()
                .Where(c => c.Subdomain.Equals(tenantName))
                .FirstOrDefaultAsync();
        }
    }
}
