using SmartServe.Common.Models;
using SmartServe.Domain.Models;

namespace SmartServe.Domain.Interfaces
{
	public interface IProductService
	{
		Task<List<Brand>> GetBrandsAsync();
		Task<List<Category>> GetActiveCategoriesAsync();
		Task<List<Category>> GetCategoriesAsync();
		Task<List<Product>> GetProductsAsync();
		Task<List<Product>> GetActiveProductsAsync();
		Task<List<ProductVariant>> GetProductVariantsAsync();
		Task<List<ProductVariant>> GetActiveProductVariantsAsync();
		Task<List<Product>> GetProductByCategoryIdAsync(int categoryId);
		Task<List<ProductVariant>> GetVariantAsync();
		Task<List<ProductVariant>> GetVariantByProductIdAsync(int productId);
		Task<List<ProductVariant>> GetVariantsByBrandIdAsync(int brandId);
		Task<List<Stock>> GetAllStockAsync();
		Task<BaseResponse> DeleteCategoryAsync(int id);
		Task<BaseResponse> DeleteProductAsync(int id);
		Task<BaseResponse> DeleteVariantAsync(int id);
		Task<BaseResponse> SaveBulkCategoriesAsync(IEnumerable<Category> items);
		Task<BaseResponse> SaveBulkProductsAsync(IEnumerable<Product> items);
		Task<BaseResponse> SaveBulkVariantAsync(IEnumerable<ProductVariant> incoming);
	}
}
