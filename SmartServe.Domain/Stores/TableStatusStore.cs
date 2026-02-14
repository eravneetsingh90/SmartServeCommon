using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using SmartServe.Resources.Provider;

namespace SmartServe.Domain.Stores
{
	public class TableStatusStore : BaseStore<TableStatusEntity>, ITableStatusStore
	{
		public TableStatusStore(SmartServeDbContext db, ITenantProvider tenantProvider) : base(db, tenantProvider) { }

		public async Task<TableStatusEntity?>  GetTableStatusByCode(string code)
		{
			return await Set.Where(ts => ts.StatusCode == code).FirstOrDefaultAsync();
		}
	}
}
