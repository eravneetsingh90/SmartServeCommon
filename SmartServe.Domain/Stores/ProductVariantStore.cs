using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class ProductVariantStore : BaseStore<ProductVariant>
	{
		public ProductVariantStore(SmartServeDbContext db) : base(db) { }
	}
}
