using AutoMapper;
using SmartServe.Domain.Constants;
using SmartServe.Domain.Stores;

namespace SmartServe.Domain.Services
{
	public class StockService : IStockService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;
		private readonly IOrderStore _orderStore;
		private readonly IStockStore _stockStore;
		private readonly IProductIngredientStore _productIngredientStore;
		public StockService(
			IMapper mapper,
			IUnitOfWork uow,
			IOrderStore orderStore,
			IOrderItemStore orderItemStore,
			IStockStore stockStore,
			IProductIngredientStore productIngredientStore)
		{
			_mapper = mapper;
			_uow = uow;
			_orderStore = orderStore;
			_stockStore = stockStore;
			_productIngredientStore = productIngredientStore;
		}
		
		public async Task ConsumeForOrderAsync(int orderId)
		{
			var order = await _orderStore.GetOrderAsync(orderId);

			if (order == null)
				throw new InvalidOperationException($"Order {orderId} not found");

			using var tx = _uow.BeginAsync();

			try
			{
				foreach (var item in order.OrderItems)
				{
					if (item.Variant.StockMode == StockMode.SEALED)
					{
						var canHandle = _stockStore.CanHandle(item.Variant);

						if (canHandle == null)
							throw new InvalidOperationException(
								$"No stock for variant {item.VariantId}");

						await _stockStore.ConsumeAsync(item, orderId);
					}
					else if (item.Variant.StockMode == StockMode.INGREDIENT)
					{
						var canHandle = _productIngredientStore.CanHandle(item.Variant);

						if (canHandle == null)
							throw new InvalidOperationException(
								$"No stock for variant {item.VariantId}");

						await _productIngredientStore.ConsumeAsync(item, orderId);
					}
					else
					{
						continue;
					}

					
				}

				await _uow.CommitAsync();
			}
			catch
			{
				await _uow.RollbackAsync();
				throw;
			}
		}

	}
}
