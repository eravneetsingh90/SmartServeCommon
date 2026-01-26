using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IOrderStore : IBaseStore<OrderEntity>
	{
		Task<OrderEntity?> GetOrderAsync(int orderId);
		void Update(OrderEntity order);
		Task<OrderEntity?> GetByOrderNumberAsync(string orderNumber);
		Task<List<OrderEntity>> GetByDateFilterAsync(DateTime fromUtc, DateTime toUtc);
	}

}
