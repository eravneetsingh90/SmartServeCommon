using SmartServe.Domain.Constants;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class StockService : IStockService
	{
		private readonly IStockStore _stockStore;
		private readonly IOrderStore _orderStore;
		private readonly IProductVariantStore _productVariantStore;
		private readonly IIngredientStore _ingredientStore;

		public StockService(
			IStockStore stockStore,
			IOrderStore orderStore,
			IProductVariantStore productVariantStore,
			IIngredientStore ingredientStore)
		{
			_stockStore = stockStore;
			_orderStore = orderStore;
			_productVariantStore = productVariantStore;
			_ingredientStore = ingredientStore;
		}

		public async Task ApplyOrderStockAsync(int orderId)
		{
			var order = await _orderStore.GetOrderAsync(orderId);
			if (order == null)
				throw new InvalidOperationException("Order not found");

			foreach (var item in order.OrderItems)
			{
				var variant = await _productVariantStore.GetByIdAsync(item.VariantId);
				if (variant == null || variant.StockMode == "NONE")
					continue;

				if (variant.StockMode == "SEALED")
				{
					await DeductSealedVariantAsync(
						variant.VariantId,
						item.Quantity,
						orderId);
				}
				else if (variant.StockMode == "INGREDIENT")
				{
					await DeductIngredientsAsync(
						variant.VariantId,
						item.Quantity,
						orderId);
				}
			}
		}

		private async Task DeductSealedVariantAsync(
			int variantId,
			int quantity,
			int orderId)
		{
			var stockItem = await _stockStore
				.GetStockItemAsync(StockMode.SEALED, variantId);

			if (stockItem == null)
				throw new InvalidOperationException(
					$"Stock item not found for variant {variantId}");

			await _stockStore.AddTransactionAsync(new StockTransaction
			{
				StockItemId = stockItem.StockItemId,
				TransactionType = StockTxnType.OUT,
				Quantity = quantity,
				Reason = "SALE",
				ReferenceType = "ORDER",
				ReferenceId = orderId,
				CreatedAt = DateTime.UtcNow
			});
		}

		private async Task DeductIngredientsAsync(
			int variantId,
			int variantQty,
			int orderId)
		{
			var recipe = await _ingredientStore
				.GetIngredientsForVariantAsync(variantId);

			foreach (var r in recipe)
			{
				var totalQty = r.QtyRequired * variantQty;

				var stockItem = await _stockStore
					.GetStockItemAsync(
						StockMode.INGREDIENT,
						r.IngredientId);

				if (stockItem == null)
					throw new InvalidOperationException(
						$"Stock item not found for ingredient {r.IngredientId}");

				await _stockStore.AddTransactionAsync(new StockTransaction
				{
					StockItemId = stockItem.StockItemId,
					TransactionType = StockTxnType.OUT,
					Quantity = totalQty,
					Reason = "SALE",
					ReferenceType = "ORDER",
					ReferenceId = orderId,
					CreatedAt = DateTime.UtcNow
				});
			}
		}

		public async Task AddStockAsync(
			string itemType,
			int referenceId,
			decimal quantity,
			string reason)
		{
			var stockItem = await _stockStore
				.GetStockItemAsync(itemType, referenceId);

			if (stockItem == null)
				throw new InvalidOperationException("Stock item not found");

			await _stockStore.AddTransactionAsync(new StockTransaction
			{
				StockItemId = stockItem.StockItemId,
				TransactionType = StockTxnType.IN,
				Quantity = quantity,
				Reason = reason,
				ReferenceType = "MANUAL",
				CreatedAt = DateTime.UtcNow
			});
		}

		public async Task AdjustStockAsync(
			int stockItemId,
			decimal quantity,
			string reason)
		{
			await _stockStore.AddTransactionAsync(new StockTransaction
			{
				StockItemId = stockItemId,
				TransactionType = StockTxnType.ADJUST,
				Quantity = quantity,
				Reason = reason,
				ReferenceType = "MANUAL",
				CreatedAt = DateTime.UtcNow
			});
		}

		public async Task EnsureStockItemAsync(
			string itemType,
			int referenceId,
			string unit)
		{
			var existing = await _stockStore
				.GetStockItemAsync(itemType, referenceId);

			if (existing != null)
				return; // already exists → SAFE EXIT

			var stockItem = new StockItem
			{
				ItemType = itemType,
				ReferenceId = referenceId,
				Unit = unit,
				IsActive = true,
				CreatedAt = DateTime.UtcNow
			};

			await _stockStore.AddStockItemAsync(stockItem);
		}

	}

}
