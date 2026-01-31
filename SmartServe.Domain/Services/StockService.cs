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
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly IStockStore _stockStore;
		private readonly IStockTransactionStore _stockTransactionStore;
		private readonly IOrderStore _orderStore;
		private readonly IProductIngredientStore _productIngredientStore;
		#endregion

		#region constructor
		public StockService(
			IMapper mapper,
			IUnitOfWork uow,
			IStockStore stockStore,
			IStockTransactionStore stockTransactionStore,
			IOrderStore orderStore,
			IProductIngredientStore productIngredientStore)
		{
			_uow = uow;
			_mapper = mapper;
			_stockStore = stockStore;
			_stockTransactionStore = stockTransactionStore;
			_orderStore = orderStore;
			_productIngredientStore = productIngredientStore;
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

		#endregion
	}

}
