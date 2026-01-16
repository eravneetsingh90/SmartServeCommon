using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class ProductIngredientStore : BaseStore<ProductIngredient>, IProductIngredientStore
	{
		public ProductIngredientStore(SmartServeDbContext db) : base(db)
		{
		}


	}
}
