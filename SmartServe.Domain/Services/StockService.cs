using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartServe.Common.Models;
using SmartServe.Domain.Constants;
using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;
using System;

namespace SmartServe.Domain.Services
{
	public class StockService : IStockService
	{
		#region fields
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly IStockStore _stockStore;
		private readonly IStockTransactionStore _stockTransactionStore;
		private readonly IOrderStore _orderStore;
		private readonly IProductIngredientStore _productIngredientStore;
		private readonly IOrderItemStore _orderItemStore;
		//private readonly ILogger<StockService> _logger;
		#endregion

		#region constructor
		public StockService(
			IMapper mapper,
			IUnitOfWork uow,
			IStockStore stockStore,
			IStockTransactionStore stockTransactionStore,
			IOrderStore orderStore,
			IProductIngredientStore productIngredientStore,
			IOrderItemStore orderItemStore)
			//ILogger<StockService> logger)
		{
			_uow = uow;
			_mapper = mapper;
			_stockStore = stockStore;
			_stockTransactionStore = stockTransactionStore;
			_orderStore = orderStore;
			_productIngredientStore = productIngredientStore;
			_orderItemStore = orderItemStore;
			//_logger = logger;
		}
		#endregion

		#region methods
		public async Task<List<Stock>> GetStockAsync()
		{
			return _mapper.Map<List<Stock>>(await _stockStore.GetStockAsync());
		}

		public async Task ActivateStockItemAsync(Stock stock)
		{
			var stockEntity = _mapper.Map<StockEntity>(stock);
			if (stockEntity.Id == 0)
			{
				_stockStore.Add(stockEntity);
			}
			else
			{
				_stockStore.Update(stockEntity);
			}

			await _stockStore.SaveAsync();
		}

		public async Task<BaseResponse> AddStockAsync(List<AddStock> rows)
		{
			var response = BaseResponse.New();
			try
			{
				await _uow.BeginAsync();
				foreach (var row in rows)
				{
					var stockItem = await _stockStore.GetStockAsync(row.ItemType.ToString(), row.VariantId);

					if (stockItem == null)
						throw new Exception("Stock item not found.");

					_stockTransactionStore.Add(new StockTransactionEntity
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
			catch (Exception ex)
			{
				response.MetaData.ResultCode = ResultCodes.Error;
				response.MetaData.ResultMessage = ResultMessages.Error;
			}
			return response;
		}

		public async Task<List<CurrentStock>> GetCurrentStockAsync()
		{
			return await _stockStore.GetCurrentStockAsync();
		}
		public async Task ProcessUntrackedOrdersAsync(CancellationToken ct = default)
		{
			var orders = await _orderStore.GetUntrackedOrdersAsync(
				max: 10,
				ct);

			foreach (var order in orders)
			{
				await _uow.BeginAsync();

				try
				{
					var orderItems = await _orderItemStore.GetOrderItemsAsync(order.Id);

					foreach (var item in orderItems)
					{
						await ProcessOrderItemAsync(order, item, ct);
					}

					order.IsTracked = true;
					_orderStore.Update(order);

					await _uow.CommitAsync();
				}
				catch (Exception ex)
				{
					await _uow.RollbackAsync();

					//_logger.LogError(
					//	ex,
					//	"Stock tracking failed for OrderId {OrderId}",
					//	order.Id);
				}
			}
		}
		#endregion

		#region Private Methods
		private async Task ProcessOrderItemAsync(
					OrderEntity order,
					OrderItemEntity item,
					CancellationToken ct)
		{
			// Variant-based stock only
			var stock = await _stockStore.GetStockAsync(item.VariantId??0);

			if (stock == null)
			{
				//_logger.LogWarning(
				//	"No stock found for VariantId {VariantId}, OrderId {OrderId}",
				//	item.VariantId,
				//	order.Id);
				return;
			}

			if (stock.ItemType != StockItemType.VARIANT)
			{
				//_logger.LogInformation(
				//	"Skipping ingredient stock. StockId {StockId}",
				//	stock.Id);
				return;
			}

			var transaction = new StockTransactionEntity
			{
				StockId = stock.Id,
				TransactionType = StockTxnType.OUT,
				Quantity = item.Quantity,
				Reason = "ORDER",
				ReferenceType = "ORDER",
				ReferenceId = order.Id
			};

			_stockTransactionStore.Add(transaction);
		}


		#endregion
	}

}
