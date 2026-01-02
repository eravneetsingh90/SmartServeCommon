using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class BrandStore: BaseStore<Brand>,IBrandStore
	{
		public BrandStore(SmartServeDbContext db) : base(db)
		{
		}
	}
}
