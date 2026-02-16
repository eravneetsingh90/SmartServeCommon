using SmartServe.Domain.Models;
using SmartServe.Domain.Security;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserStore _userStore;
        private readonly ITenantStore _tenantStore;

        public AuthService(IUserStore userStore,ITenantStore tenantStore)
        {
            _userStore = userStore;
            _tenantStore = tenantStore;
        }

        public async Task<CurrentUser?> LoginAsync(
        string username,
        string pin,
        string tenantCode)
        {
            var tenant = await _tenantStore.GetByDomainAsync(tenantCode);
            if (tenant == null)
                 return null;
            
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(pin))
                return null;

            var user = await _userStore.GetActiveUserAsync(username, tenant.Id);

            if (user == null)
                return null;

            if (!PinHasher.Verify(pin, user.PinHash))
                return null;

            return new CurrentUser
            {
                UserId = user.Id,
                TenantId = user.TenantId,
                Role = user.Role.RoleName
            };
        }
    }
}
