using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Constants;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartServe.Domain.Stores
{
	public class ProductIngredientStore : BaseStore<ProductIngredient>, IProductIngredientStore
	{
		private readonly SmartServeDbContext _db;

		public ProductIngredientStore(SmartServeDbContext db) : base(db)
		{
			_db = db;
		}
		public bool CanHandle(ProductVariant variant)
		=> variant.StockMode == StockMode.INGREDIENT;

		public async Task ConsumeAsync(OrderItem item, int orderId)
		{
			var recipe = await _db.ProductIngredients
				.Where(r => r.VariantId == item.VariantId)
				.ToListAsync();

			if (!recipe.Any())
				throw new InvalidOperationException(
					$"No ingredient recipe defined for variant {item.VariantId}");

			foreach (var ingredient in recipe)
			{
				var stock = await _db.IngredientStocks.FirstOrDefaultAsync(s => s.IngredientId == ingredient.IngredientId);

				if (stock == null)
					throw new InvalidOperationException(
						$"No stock found for ingredient {ingredient.IngredientId}");

				var requiredQty = ingredient.QtyRequired * item.Quantity;

				if (stock.Quantity < requiredQty)
					throw new InvalidOperationException(
						$"Insufficient stock for ingredient {ingredient.IngredientId}");

				stock.Quantity -= requiredQty;

				_db.IngredientTransactions.Add(new IngredientTransaction
				{
					IngredientId = ingredient.IngredientId,
					ChangeQty = -requiredQty,
					Reason = "SALE",
					ReferenceId = orderId,
					CreatedAt = DateTime.UtcNow
				});
			}
		}
	}
}
