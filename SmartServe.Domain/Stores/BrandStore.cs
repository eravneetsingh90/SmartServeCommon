using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class BrandStore: BaseStore<BrandEntity>,IBrandStore
	{
		public BrandStore(SmartServeDbContext db) : base(db)
		{
		}
	}
}
