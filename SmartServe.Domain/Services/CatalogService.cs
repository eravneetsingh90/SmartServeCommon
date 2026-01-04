using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class CatalogService : ICatalogService
	{
		private readonly ICategoryStore _categoryStore;
		private readonly IProductStore _productStore;
		private readonly IProductVariantStore _variantStore;
		private readonly ITableStatusStore _tableStatusStore;
		private readonly IBrandStore _brandStore;

		private List<Category> _categories = new();
		private List<Product> _products = new();
		private List<ProductVariant> _variants = new();
		private List<CatalogSearchItem> _searchIndex = new();
		private List<TableStatus> _tableStatus = new();
		private List<Brand> _brands = new();
		private bool _loaded;

		public CatalogService(
			ICategoryStore categoryStore,
			IProductStore productStore,
			IProductVariantStore variantStore,
			ITableStatusStore tableStatusStore,
			IBrandStore brandStore)
		{
			_categoryStore = categoryStore;
			_productStore = productStore;
			_variantStore = variantStore;
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
			_categories = (await _categoryStore.GetAllAsync())
				.Where(c => c.IsActive == true)
				.OrderBy(c => c.DisplayOrder)
				.ToList();

			// Products
			_products = (await _productStore.GetAllAsync())
				.Where(p => p.IsActive == true)
				.ToList();

			// Variants
			_variants = (await _variantStore.GetAllAsync())
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
				 select new CatalogSearchItem
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
		public IReadOnlyList<Category> GetCategories()
			=> _categories;

		public IReadOnlyList<Product> GetProductsByCategory(int categoryId)
			=> _products
				.Where(p => p.CategoryId == categoryId)
				.OrderBy(p => p.DisplayOrder)
				.ToList();

		public IReadOnlyList<ProductVariant> GetVariantsByProduct(int productId)
			=> _variants
				.Where(v => v.ProductId == productId)
				.OrderBy(v => v.DisplayOrder)
				.ToList();

		public IReadOnlyList<CatalogSearchItem> Search(string term, int maxResults = 30)
		{
			if (string.IsNullOrWhiteSpace(term))
				return Array.Empty<CatalogSearchItem>();

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
