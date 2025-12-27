using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class BillingService : IBillingService
	{
		private readonly IOrderStore _orderStore;

		public BillingService(IOrderStore orderStore)
		{
			_orderStore = orderStore;
		}

		public async Task<int> SaveOrderAsync(BillingSaveRequest request)
		{
			Order order;

			// 🔹 NEW ORDER
			if (request.OrderId == null)
			{
				order = new Order
				{
					OrderType = request.OrderType,
					TotalAmount = request.TotalAmount,
					StatusId = GetRunningStatusId(),
					CreatedAt = DateTime.UtcNow
				};

				await _orderStore.CreateOrderAsync(order);
			}
			// 🔹 UPDATE EXISTING ORDER (future-ready)
			else
			{
				order = await _orderStore.GetOrderAsync(request.OrderId.Value)
					?? throw new InvalidOperationException("Order not found");

				order.TotalAmount = request.TotalAmount;

				await _orderStore.UpdateOrderAsync(order);
				await _orderStore.ClearOrderItemsAsync(order.OrderId);
			}

			// 🔹 ADD ORDER ITEMS
			var items = request.Items.Select(x => new OrderItem
			{
				OrderId = order.OrderId,
				VariantId = x.VariantId,
				Quantity = x.Quantity,
				PriceSnapshot = x.PriceSnapshot,
				DiscountAmount = 0
			});

			await _orderStore.AddOrderItemsAsync(items);

			return order.OrderId;
		}

		// TEMP: later move to lookup / cache
		private int GetRunningStatusId() => 1;
	}
}
