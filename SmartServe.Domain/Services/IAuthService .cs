using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
    public interface IAuthService
    {
        Task<CurrentUser?> LoginAsync(string username, string pin, string tenantCode);
    }
}
