using AutoMapper;
using SmartServe.Domain.Constants;
using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class StockService : IStockService
	{
		#region fields
		private readonly IMapper _mapper;
		private readonly IStockItemStore _stockItemStore;
		private readonly IStockTransactionStore _stockTransactionStore;
		private readonly IOrderStore _orderStore;
		private readonly IIngredientStore _ingredientStore;
		#endregion

		#region constructor
		public StockService(
			IMapper mapper,
			IStockItemStore stockItemStore,
			IStockTransactionStore stockTransactionStore,
			IOrderStore orderStore,
			IIngredientStore ingredientStore)
		{
			_mapper = mapper;
			_stockItemStore = stockItemStore;
			_stockTransactionStore = stockTransactionStore;
			_orderStore = orderStore;
			_ingredientStore = ingredientStore;
		}
		#endregion

		#region methods
		public async Task<List<StockItemDto>> GetStockItemAsync(string itemType)
		{
			return _mapper.Map<List<StockItemDto>>(await _stockItemStore.GetStockItemAsync(itemType));
		}

		public async Task CreateStockItemAsync(string itemType, int referenceId, string unit, decimal minStockLevel)
		{
			var stockItem = new StockItem
			{
				ItemType = itemType,
				ReferenceId = referenceId,
				Unit = unit,
				MinStockLevel = minStockLevel,
				IsActive = true,
				CreatedAt = DateTime.UtcNow
			};

			await _stockItemStore.AddStockItemAsync(stockItem);
		}

		public async Task ActivateStockItemAsync(string itemType,int referenceId)
		{
			var stockItem = await _stockItemStore
				.GetStockItemAsync(itemType, referenceId);

			if (stockItem == null)
			{
				await CreateStockItemAsync(itemType, referenceId, "PCS", 0);
				return;
			}

			if (stockItem.IsActive == true)
				return; // already active → no-op

			stockItem.IsActive = true;

			await _stockItemStore.UpdateStockItemAsync(stockItem);
		}

		public async Task DeactivateStockItemAsync(string itemType,int referenceId)
		{
			var stockItem = await _stockItemStore.GetStockItemAsync(itemType, referenceId);

			if (stockItem == null)
				return;

			var currentQty = await _stockItemStore.GetCurrentStockQuantityAsync(stockItem.StockItemId);

			if (currentQty != 0)
				throw new InvalidOperationException(
					"Cannot remove from stock while quantity is not zero.");

			stockItem.IsActive = false;

			await _stockItemStore.UpdateStockItemAsync(stockItem);
		}

		public async Task AddStockAsync(int stockItemId,decimal quantity,string reason,string referenceType = "MANUAL",int? referenceId = null)
		{
			if (quantity <= 0)
				throw new ArgumentException("Quantity must be greater than zero.");

			_stockTransactionStore.Add(new StockTransaction
			{
				StockItemId = stockItemId,
				TransactionType = StockTxnType.IN,
				Quantity = quantity,
				Reason = reason,
				ReferenceType = referenceType,
				ReferenceId = referenceId,
				CreatedAt = DateTime.UtcNow
			});
			await _stockTransactionStore.SaveAsync();
		}

		public async Task AdjustStockAsync(int stockItemId,decimal quantity,string reason)
		{
			if (quantity == 0)
				return;

			_stockTransactionStore.Add(new StockTransaction
			{
				StockItemId = stockItemId,
				TransactionType = StockTxnType.ADJUST,
				Quantity = quantity,
				Reason = reason,
				ReferenceType = "MANUAL",
				CreatedAt = DateTime.UtcNow
			});
			await _stockTransactionStore.SaveAsync();
		}

		public async Task ApplyOrderStockAsync(int orderId)
		{
			var order = await _orderStore.GetOrderAsync(orderId);
			if (order == null)
				throw new InvalidOperationException("Order not found.");

			foreach (var item in order.OrderItems)
			{
				// 1️⃣ Check sealed stock (variant stock_item exists)
				var variantStockItem = await _stockItemStore
					.GetStockItemAsync(
						StockItemType.VARIANT,
						Convert.ToInt32(item.VariantId));

				if (variantStockItem != null)
				{
					// SEALED ITEM
					await ConsumeStockAsync(
						variantStockItem.StockItemId,
						item.Quantity,
						orderId);

					continue;
				}

				// 2️⃣ Ingredient-based deduction (recipe)
				var ingredients = await _ingredientStore
					.GetIngredientsForVariantAsync(Convert.ToInt32(item.VariantId));

				foreach (var ing in ingredients)
				{
					var ingredientStockItem = await _stockItemStore
						.GetStockItemAsync(
							StockItemType.INGREDIENT,
							ing.IngredientId);

					if (ingredientStockItem == null)
						throw new InvalidOperationException(
							$"Ingredient stock not configured.");

					var totalQty = ing.QtyRequired * item.Quantity;

					await ConsumeStockAsync(
						ingredientStockItem.StockItemId,
						totalQty,
						orderId);
				}
			}
		}

		private async Task ConsumeStockAsync(int stockItemId,decimal quantity,int orderId)
		{
			if (quantity <= 0)
				return;

			var currentQty = await _stockItemStore
				.GetCurrentStockQuantityAsync(stockItemId);

			if (currentQty < quantity)
				throw new InvalidOperationException(
					"Insufficient stock.");

			_stockTransactionStore.Add(new StockTransaction
			{
				StockItemId = stockItemId,
				TransactionType = StockTxnType.OUT,
				Quantity = quantity,
				Reason = "SALE",
				ReferenceType = "ORDER",
				ReferenceId = orderId,
				CreatedAt = DateTime.UtcNow
			});
			await _stockTransactionStore.SaveAsync();
		}

		#endregion
	}

}
