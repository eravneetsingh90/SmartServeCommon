using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Entities;

namespace SmartServe.Domain.Stores
{
	public class UserStore
	{
		private readonly SmartServeDbContext _db;

		public UserStore(SmartServeDbContext db)
		{
			_db = db;
		}

		public async Task<user1?> GetActiveUserByUsernameAsync(string username)
		{
			return await _db.users1
				.AsNoTracking()
				.FirstOrDefaultAsync(u =>
					u.name == username &&
					u.is_active);
		}
	}
}
