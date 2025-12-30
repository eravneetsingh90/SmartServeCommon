using AutoMapper;
using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class BillingService : IBillingService
	{
		private readonly IOrderStore _orderStore;
		private readonly IOrderItemStore _orderItemStore;
		private readonly ITableStatusStore _tableStatusStore;
		private readonly IMapper _mapper;


		public BillingService(IMapper mapper, IOrderStore orderStore, ITableStatusStore tableStatusStore, IOrderItemStore orderItemStore)
		{
			_mapper = mapper;
			_orderStore = orderStore;
			_tableStatusStore = tableStatusStore;
			_orderItemStore = orderItemStore;
		}

		public async Task<OrderDto> GetOrderAsync(int orderId)
		{
			var order = await _orderStore.GetOrderAsync(orderId);
			return _mapper.Map<OrderDto>(order);
		}

		public async Task UpdateOrderAsync(OrderDto order)
		{
			await _orderStore.UpdateAndSaveAsync(_mapper.Map<Order>(order));
		}

		public async Task<int> CreateOrderAsync(OrderDto request)
		{
			var order = _mapper.Map<Order>(request);

			if (order.OrderId <= 0)
			{
				order.CreatedAt = DateTime.UtcNow;
				await _orderStore.AddAndSaveAsync(order);
			}
			return order.OrderId;
		}

		public async Task CreateOrderItemsAsync(List<OrderItemDto> request)
		{
			var orderItems = _mapper.Map<List<OrderItem>>(request);

			await _orderItemStore.AddOrderItemsAsync(orderItems);

		}

		public async Task UpdateOrderItemsAsync(int orderId,List<OrderItemDto> items)
		{
			var orderItems = _mapper.Map<List<OrderItem>>(items);

			await _orderItemStore.UpdateOrderItemsAsync(orderId,orderItems);
		}

	}
}
