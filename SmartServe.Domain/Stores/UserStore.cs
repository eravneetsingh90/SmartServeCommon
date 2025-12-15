using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Entities;

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
			var result = await _db.users1
				.AsNoTracking()
				.FirstOrDefaultAsync(u =>
					u.name == username &&
					u.is_active);
			return result;
		}
	}
}
