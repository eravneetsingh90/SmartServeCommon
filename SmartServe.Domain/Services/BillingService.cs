using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class BillingService : IBillingService
	{
		public BillingService()
		{

		}

		public Task<int> SaveOrderAsync(BillingSaveRequest request)
		{
			throw new NotImplementedException();
		}
		//public async Task<int> SaveOrderAsync(BillingSaveRequest request)
		//{
		//	using var tx = await _db.Database.BeginTransactionAsync();

		//	Order order;

		//	if (request.OrderId == null)
		//	{
		//		order = CreateNewOrder(request);
		//		_db.Orders.Add(order);
		//		await _db.SaveChangesAsync();
		//	}
		//	else
		//	{
		//		order = await LoadExistingOrder(request.OrderId.Value);
		//		UpdateOrderHeader(order, request);
		//		ClearExistingItems(order.OrderId);
		//	}

		//	AddOrderItems(order, request.Items);
		//	UpdateOrderTotal(order);

		//	HandleStock(order.OrderId, request.Items);

		//	UpdateTableStatus(order);

		//	await _db.SaveChangesAsync();
		//	await tx.CommitAsync();

		//	return order.OrderId;
		//}

	}
}
