using AutoMapper;
using SmartServe.Domain.Constants;
using SmartServe.Domain.Interfaces;
using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;

namespace SmartServe.Domain.Services
{
	public class OrderReportService : IOrderReportService
	{
		private readonly IMapper _mapper;
		private readonly IOrderStore _orderStore;
		private readonly IOrderItemStore _orderItemStore;
		private readonly IPaymentStore _paymentStore;
		
		public OrderReportService(
			IMapper mapper,
			IOrderStore orderStore,
			IOrderItemStore orderItemStore,
			IPaymentStore paymentStore)
		{
			_mapper = mapper;
			_orderStore = orderStore;
			_orderItemStore = orderItemStore;
			_paymentStore = paymentStore;	
		}
		public async Task<List<OrderItem>> GetOrderItemsAsync(int orderId)
		{
			var orderItemEntities =
				await _orderItemStore.GetOrderItemsAsync(orderId);
			return _mapper.Map<List<OrderItem>>(orderItemEntities);
		}
		public async Task<OrderReportResult> GetOrdersAsync(
			DateTime fromUtc,
			DateTime toUtc)
		{
			fromUtc = NormalizeUtc(fromUtc);
			toUtc = NormalizeUtc(toUtc);

			var orderEntities =	await _orderStore.GetByDateFilterAsync(fromUtc, toUtc);
			var paymentEntities = await _paymentStore.GetByDateFilterAsync(fromUtc, toUtc);
			
			var orders = _mapper.Map<List<Order>>(orderEntities);
			var payments = _mapper.Map<List<Payment>>(paymentEntities);

			return new OrderReportResult
			{
				TotalOrders = orders.Count,
				TotalSales = orders.Sum(o => o.TotalAmount) ??0,
				Orders = orders,
				CashSales = payments.Where(p => p.Mode == PaymentMode.CASH).Sum(p => p.Amount)??0,
				UpiSales = payments.Where(p => p.Mode == PaymentMode.UPI).Sum(p => p.Amount) ?? 0,
			};
		}

		#region helpers

		private static DateTime NormalizeUtc(DateTime dt)
		{
			return dt.Kind switch
			{
				DateTimeKind.Utc => dt,
				DateTimeKind.Local => dt.ToUniversalTime(),
				DateTimeKind.Unspecified => DateTime.SpecifyKind(dt, DateTimeKind.Utc),
				_ => dt
			};
		}

		#endregion
	}
}
