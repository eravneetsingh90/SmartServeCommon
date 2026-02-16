using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
    public interface ITenantStore : IBaseStore<TenantEntity>
    {
        Task<TenantEntity?> GetByDomainAsync(string tenantName);
    }
}
