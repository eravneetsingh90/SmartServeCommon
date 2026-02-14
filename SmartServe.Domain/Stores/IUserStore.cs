using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
    public interface IUserStore : IBaseStore<UserEntity>
    {
        Task<UserEntity?> GetActiveUserAsync(string username, int tenantId);
        //Task<UserEntity?> GetActiveUserByUsernameAsync(string username);
    }
}
