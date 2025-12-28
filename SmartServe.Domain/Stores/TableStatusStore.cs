using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class TableStatusStore : BaseStore<TableStatus>, ITableStatusStore
	{
		public TableStatusStore(SmartServeDbContext db) : base(db) { }

		public async Task<TableStatus?>  GetTableStatusByCode(string code)
		{
			return await _db.TableStatuses.Where(ts => ts.StatusCode == code).FirstOrDefaultAsync();
		}
	}
}
