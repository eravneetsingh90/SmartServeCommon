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
		public async Task<IEnumerable<ProductIngredient>> GetIngredientsForVariantAsync(
			int variantId)
		{
			return await Db.ProductIngredients
				.Where(x => x.VariantId == variantId)
				.Include(x => x.Ingredient)
				.AsNoTracking()
				.ToListAsync();
		}
	}
}
