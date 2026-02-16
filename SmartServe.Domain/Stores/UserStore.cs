using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class UserStore : BaseStore<UserEntity>, IUserStore
	{
		public UserStore(SmartServeDbContext db)
			: base(db)
		{
		}

        public async Task<UserEntity?> GetActiveUserAsync(
        string username,
        int tenantId)
        {
            return await Set
                .AsNoTracking()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u =>
                    u.Name == username &&
                    u.TenantId == tenantId &&
                    u.IsActive == true);
        }

        //      public async Task<UserEntity?> GetActiveUserByUsernameAsync(string username)
        //{
        //	var result = await Set
        //		.AsNoTracking()
        //		.Include(u=>u.Role)
        //		.FirstOrDefaultAsync(u =>
        //			u.Name == username &&
        //			u.IsActive == true);
        //	return result;
        //}
    }
}
