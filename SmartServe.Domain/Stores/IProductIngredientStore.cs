using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IProductIngredientStore : IBaseStore<ProductIngredient>
	{
		bool CanHandle(ProductVariant variant);
		Task ConsumeAsync(OrderItem item, int orderId);
	}
}
