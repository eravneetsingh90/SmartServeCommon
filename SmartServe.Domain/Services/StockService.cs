using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmartServe.Domain.Constants;
using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class StockService : IStockService
	{
		#region fields
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly IStockStore _stockItemStore;
		private readonly IStockTransactionStore _stockTransactionStore;
		private readonly IOrderStore _orderStore;
		private readonly IIngredientStore _ingredientStore;
		private readonly IProductIngredientStore _productIngredientStore;
		#endregion

		#region constructor
		public StockService(
			IMapper mapper,
			IUnitOfWork uow,
			IStockStore stockItemStore,
			IStockTransactionStore stockTransactionStore,
			IOrderStore orderStore,
			IIngredientStore ingredientStore,
			IProductIngredientStore productIngredientStore)
		{
			_uow = uow;
			_mapper = mapper;
			_stockItemStore = stockItemStore;
			_stockTransactionStore = stockTransactionStore;
			_orderStore = orderStore;
			_ingredientStore = ingredientStore;
			_productIngredientStore = productIngredientStore;
		}
		#endregion

		#region methods
		public async Task<List<StockDto>> GetStockItemAsync(string itemType)
		{
			return _mapper.Map<List<StockDto>>(await _stockItemStore.GetStockAsync(itemType));
		}

		public async Task CreateStockItemAsync(string itemType, int referenceId, string unit, decimal minStockLevel)
		{
			var stockItem = new Stock
			{
				ItemType = itemType,
				VariantId = referenceId,
				Unit = unit,
				MinStockLevel = minStockLevel,
				IsActive = true,
				CreatedAt = DateTime.UtcNow
			};

			await _stockItemStore.AddStockAsync(stockItem);
		}

		public async Task ActivateStockItemAsync(string itemType, int referenceId)
		{
			var stockItem = await _stockItemStore
				.GetStockAsync(itemType, referenceId);

			if (stockItem == null)
			{
				await CreateStockItemAsync(itemType, referenceId, "PCS", 0);
				return;
			}

			if (stockItem.IsActive == true)
				return; // already active → no-op

			stockItem.IsActive = true;

			await _stockItemStore.UpdateStockAsync(stockItem);
		}

		public async Task DeactivateStockItemAsync(string itemType, int referenceId)
		{
			var stockItem = await _stockItemStore.GetStockAsync(itemType, referenceId);

			if (stockItem == null)
				return;

			var currentQty = await _stockItemStore.GetCurrentStockQuantityAsync(stockItem.Id);

			if (currentQty != 0)
				throw new InvalidOperationException(
					"Cannot remove from stock while quantity is not zero.");

			stockItem.IsActive = false;

			await _stockItemStore.UpdateStockAsync(stockItem);
		}

		public async Task AddStockAsync(List<AddStockDto> rows)
		{
			await _uow.BeginAsync();
			foreach (var row in rows)
			{
				var stockItem = await _stockItemStore.GetStockAsync(row.ItemType.ToString(), row.ReferenceId);

				if (stockItem == null)
					throw new Exception("Stock item not found.");

				_stockTransactionStore.Add(new StockTransaction
				{
					StockId = stockItem.Id,
					TransactionType = StockTxnType.IN,
					Quantity = row.Quantity,
					Reason = row.Reason,
					CreatedAt = DateTime.UtcNow
				});
			}

			await _stockTransactionStore.SaveAsync();
			await _uow.CommitAsync();
		}

		public async Task AdjustStockAsync(int stockItemId, decimal quantity, string reason)
		{
			if (quantity == 0)
				return;

			_stockTransactionStore.Add(new StockTransaction
			{
				Id = stockItemId,
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
				var variantStockItem = await _stockItemStore
					.GetStockAsync(
						StockItemType.VARIANT,
						Convert.ToInt32(item.VariantId));

				if (variantStockItem != null)
				{
					await ConsumeStockAsync(
						variantStockItem.Id,
						item.Quantity,
						orderId);

					continue;
				}

				var ingredients = await _productIngredientStore.GetIngredientsForVariantAsync(Convert.ToInt32(item.VariantId));

				foreach (var ing in ingredients)
				{
					var ingredientStockItem = await _stockItemStore
						.GetStockAsync(
							StockItemType.INGREDIENT,
							ing.IngredientId);

					if (ingredientStockItem == null)
						throw new InvalidOperationException(
							$"Ingredient stock not configured.");

					var totalQty = ing.QtyRequired * item.Quantity;

					await ConsumeStockAsync(
						ingredientStockItem.Id,
						totalQty,
						orderId);
				}
			}
		}

		private async Task ConsumeStockAsync(int stockItemId, decimal quantity, int orderId)
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
				Id = stockItemId,
				TransactionType = StockTxnType.OUT,
				Quantity = quantity,
				Reason = "SALE",
				ReferenceType = "ORDER",
				ReferenceId = orderId,
				CreatedAt = DateTime.UtcNow
			});
			await _stockTransactionStore.SaveAsync();
		}

		public async Task<List<IngredientDto>> GetIngredients()
		{
			var items = await _ingredientStore.GetAllAsync();
			return _mapper.Map<List<IngredientDto>>(items);
		}

		#endregion
	}

}
