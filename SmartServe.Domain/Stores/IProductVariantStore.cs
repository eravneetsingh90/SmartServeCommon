using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IProductVariantStore : IBaseStore<ProductVariant>
	{
		Task<List<ProductVariant>> GetByProductIdAsync(int productId);
		Task<List<ProductVariant>> GetByBrandIdAsync(int brandId);
		Task<IEnumerable<ProductVariant>> SaveBulkAsync(IEnumerable<ProductVariant> incoming);
		Task DeleteAsync(int id);
	}
}
