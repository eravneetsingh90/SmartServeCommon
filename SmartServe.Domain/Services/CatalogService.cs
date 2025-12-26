using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class CatalogService : ICatalogService
	{
		private readonly ICategoryStore _categoryStore;
		private readonly ProductStore _productStore;
		private readonly ProductVariantStore _variantStore;

		private List<Category> _categories = new();
		private List<Product> _products = new();
		private List<ProductVariant> _variants = new();
		private List<CatalogSearchItem> _searchIndex = new();

		private bool _loaded;

		public CatalogService(
			ICategoryStore categoryStore,
			ProductStore productStore,
			ProductVariantStore variantStore)
		{
			_categoryStore = categoryStore;
			_productStore = productStore;
			_variantStore = variantStore;
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

			// 🔍 Build search index
			_searchIndex =
				(from v in _variants
				 join p in _products on v.ProductId equals p.ProductId
				 join c in _categories on p.CategoryId equals c.CategoryId
				 select new CatalogSearchItem
				 {
					 CategoryId = c.CategoryId,
					 ProductId = p.ProductId,
					 VariantId = v.ProductVariantId,
					 CategoryName = c.Name,
					 ProductName = p.Name,
					 VariantName = v.Name,
					 Price = v.Price,
					 SearchText =
						 (c.Name + " " + p.Name + " " + v.Name).ToLower()
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

		public void Reset()
		{
			_loaded = false;
			_categories.Clear();
			_products.Clear();
			_variants.Clear();
			_searchIndex.Clear();
		}
	}

}
