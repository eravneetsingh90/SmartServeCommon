using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class UserStore : BaseStore<User>
	{
		public UserStore(SmartServeDbContext db)
			: base(db)
		{
		}

		public async Task<User?> GetActiveUserByUsernameAsync(string username)
		{
			var result = await _db.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(u =>
					u.Name == username &&
					u.IsActive == true);
			return result;
		}
	}
}
