using SmartServe.Domain.Models;

namespace SmartServe.Domain.Services
{
	public interface IProductService
	{
		Task<List<BrandDto>> GetBrandsAsync();
		Task<List<CategoryDto>> GetCategoriesAsync();
		Task<List<ProductDto>> GetProductsAsync();
		Task<List<ProductVariantDto>> GetProductVariantsAsync();
		Task<List<ProductDto>> GetProductByCategoryIdAsync(int categoryId);
		Task<List<ProductVariantDto>> GetVariantByProductIdAsync(int productId);
		Task<List<ProductVariantDto>> GetVariantsByBrandIdAsync(int brandId);
		Task<List<StockDto>> GetAllStockAsync();
		Task<BaseResponse> DeleteCategoryAsync(int id);
		Task<BaseResponse> DeleteProductAsync(int id);
		Task<BaseResponse> DeleteVariantAsync(int id);
		Task<BaseResponse> SaveBulkCategoriesAsync(IEnumerable<CategoryDto> items);
		Task<BaseResponse> SaveBulkProductsAsync(IEnumerable<ProductDto> items);
		Task<BaseResponse> SaveBulkVariantAsync(IEnumerable<ProductVariantDto> incoming);
	}
}
