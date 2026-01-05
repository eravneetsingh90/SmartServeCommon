using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class CatalogService : ICatalogService
	{
		#region fields
		private readonly IProductService _productService;
		#endregion
		private readonly ITableStatusStore _tableStatusStore;
		private readonly IBrandStore _brandStore;

		private List<CategoryDto> _categories = new();
		private List<ProductDto> _products = new();
		private List<ProductVariantDto> _variants = new();
		private List<CatalogSearchItemDto> _searchIndex = new();
		private List<TableStatus> _tableStatus = new();
		private List<Brand> _brands = new();
		private bool _loaded;

		public CatalogService(
			IProductService productService,
			ICategoryStore categoryStore,
			IProductStore productStore,
			IProductVariantStore variantStore,
			ITableStatusStore tableStatusStore,
			IBrandStore brandStore)
		{
			_productService = productService;
			_tableStatusStore = tableStatusStore;
			_brandStore = brandStore;
		}

		// =============================
		// LOAD EVERYTHING ONCE
		// =============================
		public async Task LoadAsync()
		{
			if (_loaded)
				return;

			// Categories
			_categories = (await _productService.GetCategoriesAsync())
				.Where(c => c.IsActive == true)
				.OrderBy(c => c.DisplayOrder)
				.ToList();

			// Products
			_products = (await _productService.GetProductsAsync())
				.Where(p => p.IsActive == true)
				.ToList();

			// Variants
			_variants = (await _productService.GetProductVariantsAsync())
				.Where(v => v.IsActive == true)
				.ToList();

			// Table Statuses
			_tableStatus = (await _tableStatusStore.GetAllAsync()).ToList();

			_brands = (await _brandStore.GetAllAsync())
				.Where(v => v.IsActive == true)
				.ToList();

			// 🔍 Build search index
			_searchIndex =
				(from v in _variants
				 join p in _products on v.ProductId equals p.ProductId
				 join c in _categories on p.CategoryId equals c.CategoryId
				 select new CatalogSearchItemDto
				 {
					 CategoryId = c.CategoryId,
					 ProductId = p.ProductId,
					 VariantId = v.VariantId,
					 CategoryName = c.Name,
					 ProductName = p.Name,
					 VariantName = v.VariantName,
					 Price = v.Price,
					 SearchText =
						 (c.Name + " " + p.Name + " " + v.VariantName).ToLower()
				 })
				.ToList();

			_loaded = true;
		}

		// =============================
		// READ FROM MEMORY ONLY
		// =============================
		public IReadOnlyList<CategoryDto> GetCategories()
			=> _categories;

		public IReadOnlyList<ProductDto> GetProductsByCategory(int categoryId)
			=> _products
				.Where(p => p.CategoryId == categoryId)
				.OrderBy(p => p.DisplayOrder)
				.ToList();

		public IReadOnlyList<ProductVariantDto> GetVariantsByProduct(int productId)
			=> _variants
				.Where(v => v.ProductId == productId)
				.OrderBy(v => v.DisplayOrder)
				.ToList();

		public IReadOnlyList<CatalogSearchItemDto> Search(string term, int maxResults = 30)
		{
			if (string.IsNullOrWhiteSpace(term))
				return Array.Empty<CatalogSearchItemDto>();

			term = term.Trim().ToLower();

			return _searchIndex
				.Where(x => x.SearchText.Contains(term))
				.Take(maxResults)
				.ToList();
		}

		public async Task Refresh()
		{
			_loaded = false;
			await LoadAsync();
		}

		public TableStatus GetTableStatusByCode(string statusCode)
			=> _tableStatus
				.Where(v => v.StatusCode.Equals(statusCode)).FirstOrDefault();

		public IReadOnlyList<Brand> GetBrands()
			=> _brands;
	}

}
