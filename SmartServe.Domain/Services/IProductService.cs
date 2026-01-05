using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public interface IProductService
	{
		Task<List<CategoryDto>> GetCategoriesAsync();
		Task<List<ProductDto>> GetProductsAsync();
		Task<List<ProductVariantDto>> GetProductVariantsAsync();
		Task<List<ProductDto>> GetProductByCategoryIdAsync(int categoryId);
		Task<List<ProductVariantDto>> GetVariantByProductIdAsync(int productId);
		Task DeleteVariantAsync(int id);
		Task SaveBulkVariantAsync(IEnumerable<ProductVariantDto> incoming);
	}
}
