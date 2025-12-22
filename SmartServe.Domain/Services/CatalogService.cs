using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Models;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class CatalogService : ICatalogService
	{
		private readonly SmartServeDbContext _db;

		private List<Category> _categories = new();
		private List<Product> _products = new();
		private List<ProductVariant> _variants = new();
		private List<CatalogSearchItem> _searchIndex = new();

		private bool _loaded;

		public CatalogService(SmartServeDbContext db)
		{
			_db = db;
		}

		// =============================
		// LOAD EVERYTHING ONCE
		// =============================
		public async Task LoadAsync()
		{
			if (_loaded)
				return;

			// Categories
			_categories = await _db.Categories
				.AsNoTracking()
				.Where(c => c.IsActive == true)
				.OrderBy(c => c.DisplayOrder)
				.ToListAsync();

			// Products
			_products = await _db.Products
				.AsNoTracking()
				.Where(p => p.IsActive == true)
				.ToListAsync();

			// Variants
			_variants = await _db.ProductVariants
				.AsNoTracking()
				.Where(v => v.IsActive == true)
				.ToListAsync();

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
			=> _products.Where(p => p.CategoryId == categoryId).ToList();

		public IReadOnlyList<ProductVariant> GetVariantsByProduct(int productId)
			=> _variants.Where(v => v.ProductId == productId).ToList();

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
	}

}
