using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public interface ICatalogService
	{
		Task LoadAsync();
		IReadOnlyList<CategoryDto> GetCategories();
		IReadOnlyList<ProductDto> GetProductsByCategory(int categoryId);
		IReadOnlyList<ProductVariantDto> GetVariantsByProduct(int productId);
		IReadOnlyList<CatalogSearchItemDto> Search(string term, int maxResults = 30);
		TableStatusDto GetTableStatusByCode(string statusCode);
		Task Refresh();
		IReadOnlyList<BrandDto> GetBrands();
	}

}
