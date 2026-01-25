using AutoMapper;
using SmartServe.Domain.Constants;
using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class BillingService : IBillingService
	{
		private readonly IUnitOfWork _uow;
		private readonly IOrderStore _orderStore;
		private readonly IOrderItemStore _orderItemStore;
		private readonly IPaymentStore _paymentStore;
		private readonly IMapper _mapper;
		private readonly ITableStatusStore _tableStatusStore;

		public BillingService(
			IMapper mapper,
			IOrderStore orderStore,
			IOrderItemStore orderItemStore,
			IUnitOfWork uow,
			IPaymentStore paymentStore,
			ITableStatusStore tableStatusStore)
		{
			_mapper = mapper;
			_orderStore = orderStore;
			_orderItemStore = orderItemStore;
			_uow = uow;
			_paymentStore = paymentStore;
			_tableStatusStore = tableStatusStore;
		}

		public async Task<Order> GetOrderAsync(int orderId)
		{
			var order = await _orderStore.GetOrderAsync(orderId);
			return _mapper.Map<Order>(order);
		}

		public async Task UpdateOrderAsync(Order order)
		{
			_orderStore.Update(_mapper.Map<OrderEntity>(order));
			await _orderStore.SaveAsync();
		}

		public async Task<int> CreateOrderAsync(Order request)
		{
			if (!string.IsNullOrWhiteSpace(request.OrderNumber))
			{
				var existingOrder = await _orderStore.GetByOrderNumberAsync(request.OrderNumber);
				if (existingOrder != null)
					return existingOrder.Id;
			}
			var order = _mapper.Map<OrderEntity>(request);
			order.CreatedAt = ToUtc(order.CreatedAt ?? DateTime.UtcNow);
			order.ClosedAt = ToUtc(order.ClosedAt ?? DateTime.UtcNow);
			_orderStore.Add(order);
			await _orderStore.SaveAsync();
			return order.Id;
		}

		private static DateTime ToUtc(DateTime dt)
		{
			return dt.Kind switch
			{
				DateTimeKind.Utc => dt,
				DateTimeKind.Local => dt.ToUniversalTime(),
				DateTimeKind.Unspecified => DateTime.SpecifyKind(dt, DateTimeKind.Utc),
				_ => dt
			};
		}
		public async Task CreateOrderItemsAsync(List<OrderItem> request)
		{
			var orderItems = _mapper.Map<List<OrderItemEntity>>(request);

			await _orderItemStore.AddOrderItemsAsync(orderItems);

		}

		public async Task UpdateOrderItemsAsync(int orderId, List<OrderItem> items)
		{
			var orderItems = _mapper.Map<List<OrderItemEntity>>(items);

			await _orderItemStore.UpdateOrderItemsAsync(orderId, orderItems);
		}

		public async Task CloseOrderAsync(int orderId, Payment payment)
		{
			await _uow.BeginAsync();
			try
			{
				var order = await GetOrderAsync(orderId);

				payment.CreatedAt = DateTime.UtcNow;

				if (payment.Mode != PaymentMode.PART)
				{
					var finalPayment = _mapper.Map<PaymentEntity>(payment);
					finalPayment.OrderId = order.Id;
					finalPayment.CreatedAt = DateTime.UtcNow;
					_paymentStore.Add(finalPayment);
					await _paymentStore.SaveAsync();
				}
				else
				{
					var payments = new List<PaymentEntity>();
					payments.Add(new PaymentEntity
					{
						OrderId = order.Id,
						Mode = PaymentMode.CASH,
						Amount = payment.PartPaymentCash,
						CreatedAt = DateTime.UtcNow
					});
					payments.Add(new PaymentEntity
					{
						OrderId = order.Id,
						Mode = PaymentMode.UPI,
						Amount = payment.Amount - payment.PartPaymentCash,
						CreatedAt = DateTime.UtcNow
					});
					_paymentStore.AddRange(payments);
				}
				await _paymentStore.SaveAsync();
				order.ClosedAt = DateTime.UtcNow;
				order.StatusId = (await _tableStatusStore.GetAllAsync()).FirstOrDefault(a=>a.StatusCode.Equals(TableStatusCodes.BLANK)).Id;

				var finalOrder = _mapper.Map<OrderEntity>(order);

				_orderStore.Update(finalOrder);
				await _orderStore.SaveAsync();
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
