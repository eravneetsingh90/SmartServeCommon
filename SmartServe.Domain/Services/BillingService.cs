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
			//var items = orderItems.Select(x => new OrderItem
			//{
			//	OrderId = orderId,
			//	VariantId = x.VariantId,
			//	Quantity = x.Quantity,
			//	PriceSnapshot = x.PriceSnapshot,
			//	DiscountAmount = 0
			//});
			var orderItems = _mapper.Map<List<OrderItem>>(request);

			await _orderItemStore.AddOrderItemsAsync(orderItems);

		}

	//	public async Task UpdateOrderItemsAsync(
	//int orderId,
	//List<OrderItemDto> incomingItems)
	//	{
	//		var order = GetOrderAsync(orderId);

	//		if (order == null)
	//			throw new Exception("Open order not found");

	//		foreach (var dto in incomingItems)
	//		{
	//			// 🔹 UPDATE existing
	//			if (dto.OrderItemId>0)
	//			{
	//				var existing = order.order.OrderItems
	//					.FirstOrDefault(x => x.OrderItemId == dto.OrderItemId.Value);

	//				if (existing == null)
	//					throw new Exception("Order item not found");

	//				if (dto.Quantity <= 0)
	//				{
	//					_db.OrderItems.Remove(existing);
	//				}
	//				else
	//				{
	//					existing.Quantity = dto.Quantity;
	//					existing.PriceSnapshot = dto.PriceSnapshot;
	//				}
	//			}
	//			// 🔹 ADD new
	//			else
	//			{
	//				order.OrderItems.Add(new OrderItem
	//				{
	//					VariantId = dto.VariantId,
	//					Quantity = dto.Quantity,
	//					PriceSnapshot = dto.PriceSnapshot,
	//					DiscountAmount = 0
	//				});
	//			}
	//		}

	//		// 3️⃣ REMOVE items missing from request
	//		var incomingIds = incomingItems
	//			.Where(x => x.OrderItemId.HasValue)
	//			.Select(x => x.OrderItemId!.Value)
	//			.ToHashSet();

	//		var toRemove = order.OrderItems
	//			.Where(x => x.OrderItemId > 0 && !incomingIds.Contains(x.OrderItemId))
	//			.ToList();

	//		_db.OrderItems.RemoveRange(toRemove);

	//		// 4️⃣ Recalculate totals
	//		order.TotalAmount = order.OrderItems
	//			.Sum(x => x.Quantity * x.PriceSnapshot);

	//		// 5️⃣ Save
	//		await _db.SaveChangesAsync();
	//	}


	}
}
