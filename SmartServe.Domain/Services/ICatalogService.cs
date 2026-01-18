using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public interface ICatalogService
	{
		Task LoadAsync();
		IReadOnlyList<Category> GetCategories();
		IReadOnlyList<Product> GetProductsByCategory(int categoryId);
		IReadOnlyList<ProductVariant> GetVariantsByProduct(int productId);
		IReadOnlyList<CatalogSearchItem> Search(string term, int maxResults = 30);
		TableStatus GetTableStatusByCode(string statusCode);
		Task Refresh();
		IReadOnlyList<BrandDto> GetBrands();
	}

}
