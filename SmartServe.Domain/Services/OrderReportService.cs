using AutoMapper;
using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;

namespace SmartServe.Domain.Services
{
	public class OrderReportService : IOrderReportService
	{
		private readonly IMapper _mapper;
		private readonly IOrderStore _orderStore;

		public OrderReportService(
			IMapper mapper,
			IOrderStore orderStore)
		{
			_mapper = mapper;
			_orderStore = orderStore;
		}

		public async Task<OrderReportResult> GetOrdersAsync(
			DateTime fromUtc,
			DateTime toUtc)
		{
			fromUtc = NormalizeUtc(fromUtc);
			toUtc = NormalizeUtc(toUtc);

			var orderEntities =
				await _orderStore.GetByDateFilterAsync(fromUtc, toUtc);

			var totalOrders = orderEntities.Count;
			var totalSales = orderEntities.Sum(o => o.TotalAmount);

			var orders = _mapper.Map<List<Order>>(orderEntities);

			return new OrderReportResult
			{
				TotalOrders = totalOrders,
				TotalSales = totalSales??0,
				Orders = orders
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
