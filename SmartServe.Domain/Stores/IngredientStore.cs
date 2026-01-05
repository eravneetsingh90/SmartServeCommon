using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class IngredientStore : BaseStore<ProductIngredient>, IIngredientStore
	{
		public IngredientStore(SmartServeDbContext db) : base(db)
		{
		}

		public async Task<IEnumerable<ProductIngredient>> GetIngredientsForVariantAsync(int variantId)
		{
			return await Set
				.Include(pi => pi.Ingredient)
				.Where(pi =>
					pi.VariantId == variantId &&
					pi.Ingredient.IsActive == true)
				.AsNoTracking()
				.ToListAsync();
		}

		public Task UpdateIngredientStockAsync(int ingredientId, decimal newQty)
		{
			throw new NotSupportedException(
				"Direct ingredient stock updates are not supported. " +
				"Use StockService to manage stock via stock_transactions.");
		}

		public Task AddIngredientTransactionAsync(IngredientTransaction txn)
		{
			throw new NotSupportedException(
				"Ingredient transactions are deprecated. " +
				"Use StockService.AddStockAsync / ApplyOrderStockAsync instead.");
		}
	}

}
