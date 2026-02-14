using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using SmartServe.Resources.Provider;

namespace SmartServe.Domain.Stores
{
	public class BrandStore: BaseStore<BrandEntity>,IBrandStore
	{
		public BrandStore(SmartServeDbContext db, ITenantProvider tenantProvider) : base(db, tenantProvider)
		{
		}
	}
}
