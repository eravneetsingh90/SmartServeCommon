using AutoMapper;
using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class BillingService : IBillingService
	{
		private readonly IOrderStore _orderStore;
		private readonly ITableStatusStore _tableStatusStore;
		private readonly IMapper _mapper;


		public BillingService(IMapper mapper, IOrderStore orderStore, ITableStatusStore tableStatusStore)
		{
			_mapper = mapper;
			_orderStore = orderStore;
			_tableStatusStore = tableStatusStore;
		}

		public async Task<OrderDto> GetOrderAsync(int orderId)
		{
			var order = await _orderStore.GetOrderAsync(orderId);
			return _mapper.Map<OrderDto>(order);
		}

		public async Task UpdateOrderAsync(OrderDto dto)
		{
			var order = await _orderStore.GetOrderAsync(dto.OrderId);

			if (order == null)
				throw new Exception("Order not found");

			_mapper.Map(dto, order);
			await _orderStore.UpdateAndSaveAsync(order);
		}

		public async Task<int> CreateOrderAsync(OrderDto request)
		{
			var order = _mapper.Map<Order>(request);

			if (request.OrderId <= 0)
			{
				order.CreatedAt = DateTime.UtcNow;
				await _orderStore.AddAndSaveAsync(order);

			// 🔹 ADD ORDER ITEMS
			var items = request.OrderItems.Select(x => new OrderItem
			{
				OrderId = order.OrderId,
				VariantId = x.VariantId,
				Quantity = x.Quantity,
				PriceSnapshot = x.PriceSnapshot,
				DiscountAmount = 0
			});

			await _orderStore.AddOrderItemsAsync(items);

			
			}
			return order.OrderId;
		}

	}
}
