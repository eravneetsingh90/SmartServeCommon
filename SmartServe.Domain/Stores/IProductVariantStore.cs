using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IProductVariantStore : IBaseStore<ProductVariant>
	{
		Task<List<ProductVariant>> GetProductsVariantByProductAsync(int productId);
		Task SaveBulkProductVariantsAsync(IEnumerable<ProductVariant> productvariant);
	}
}
