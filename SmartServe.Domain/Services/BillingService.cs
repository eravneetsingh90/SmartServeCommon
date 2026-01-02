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
		private readonly ICatalogService _catalogService;
		private readonly IMapper _mapper;


		public BillingService(
			IMapper mapper,
			IOrderStore orderStore,
			ITableStatusStore tableStatusStore,
			IOrderItemStore orderItemStore,
			IUnitOfWork uow,
			IPaymentStore paymentStore,
			ICatalogService catalogService)
		{
			_mapper = mapper;
			_orderStore = orderStore;
			_orderItemStore = orderItemStore;
			_uow = uow;
			_paymentStore = paymentStore;
			_catalogService = catalogService;
		}

		public async Task<OrderDto> GetOrderAsync(int orderId)
		{
			var order = await _orderStore.GetOrderAsync(orderId);
			return _mapper.Map<OrderDto>(order);
		}

		public async Task UpdateOrderAsync(OrderDto order)
		{
			_orderStore.Update(_mapper.Map<Order>(order));
			await _orderStore.SaveAsync();
		}

		public async Task<int> CreateOrderAsync(OrderDto request)
		{
			var order = _mapper.Map<Order>(request);
			order.CreatedAt = DateTime.UtcNow;
			_orderStore.Add(order);
			await _orderStore.SaveAsync();
			return order.OrderId;
		}

		public async Task CreateOrderItemsAsync(List<OrderItemDto> request)
		{
			var orderItems = _mapper.Map<List<OrderItem>>(request);

			await _orderItemStore.AddOrderItemsAsync(orderItems);

		}

		public async Task UpdateOrderItemsAsync(int orderId, List<OrderItemDto> items)
		{
			var orderItems = _mapper.Map<List<OrderItem>>(items);

			await _orderItemStore.UpdateOrderItemsAsync(orderId, orderItems);
		}

		public async Task CloseOrderAsync(int orderId, PaymentDto payment)
		{
			await _uow.BeginAsync();
			try
			{
				var order = await GetOrderAsync(orderId);

				payment.CreatedAt = DateTime.UtcNow;

				if (payment.Mode != PaymentMode.PART)
				{
					var finalPayment = _mapper.Map<Payment>(payment);
					finalPayment.OrderId = order.OrderId;
					finalPayment.CreatedAt = DateTime.UtcNow;
					_paymentStore.Add(finalPayment);
					await _paymentStore.SaveAsync();
				}
				else
				{
					var payments = new List<Payment>();
					payments.Add(new Payment
					{
						OrderId = order.OrderId,
						Mode = PaymentMode.CASH,
						Amount = payment.PartPaymentCash,
						CreatedAt = DateTime.UtcNow
					});
					payments.Add(new Payment
					{
						OrderId = order.OrderId,
						Mode = PaymentMode.UPI,
						Amount = payment.Amount - payment.PartPaymentCash,
						CreatedAt = DateTime.UtcNow
					});
					_paymentStore.AddRange(payments);
				}
				await _paymentStore.SaveAsync();
				order.ClosedAt = DateTime.UtcNow;
				order.StatusId = _catalogService.GetTableStatusByCode(TableStatusCodes.BLANK).StatusId;

				var finalOrder = _mapper.Map<Order>(order);

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
