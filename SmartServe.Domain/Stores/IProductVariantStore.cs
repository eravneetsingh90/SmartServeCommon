using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IProductVariantStore : IBaseStore<ProductVariantEntity>
	{
		Task<List<ProductVariantEntity>> GetByProductIdAsync(int productId);
		Task<List<ProductVariantEntity>> GetByBrandIdAsync(int brandId);
		Task<IEnumerable<ProductVariantEntity>> SaveBulkAsync(IEnumerable<ProductVariantEntity> incoming);
		Task DeleteAsync(int id);
	}
}
