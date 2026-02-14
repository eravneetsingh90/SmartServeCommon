using SmartServe.Domain.Models;
using SmartServe.Domain.Security;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserStore _userStore;

        public AuthService(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public async Task<CurrentUser?> LoginAsync(
        string username,
        string pin,
        int tenantId,
        bool isOffline)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(pin))
                return null;

            var user = await _userStore.GetActiveUserAsync(username, tenantId);

            if (user == null)
                return null;

            if (!PinHasher.Verify(pin, user.PinHash))
                return null;

            return new CurrentUser
            {
                UserId = user.Id,
                TenantId = user.TenantId,
                Role = user.Role?.RoleName ?? "CASHIER",
                IsOffline = isOffline
            };
        }
    }
}
