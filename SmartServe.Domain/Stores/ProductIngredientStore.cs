using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using SmartServe.Resources.Provider;

namespace SmartServe.Domain.Stores
{
	public class ProductIngredientStore : BaseStore<ProductIngredientEntity>, IProductIngredientStore
	{
		public ProductIngredientStore(SmartServeDbContext db, ITenantProvider tenantProvider) : base(db, tenantProvider)
		{
		}


	}
}
