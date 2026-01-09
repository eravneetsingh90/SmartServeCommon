using AutoMapper;
using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;

namespace SmartServe.Domain.Services
{
	public class CatalogService : ICatalogService
	{
		#region fields
		private readonly IMapper _mapper;
		private readonly IProductService _productService;
		private readonly ITableStatusStore _tableStatusStore;
		private List<CategoryDto> _categories = new();
		private List<ProductDto> _products = new();
		private List<ProductVariantDto> _variants = new();
		private List<CatalogSearchItemDto> _searchIndex = new();
		private List<TableStatusDto> _tableStatus = new();
		private List<BrandDto> _brands = new();
		private bool _loaded;
		#endregion

		public CatalogService(
			IMapper mapper,
			IProductService productService,
			ITableStatusStore tableStatusStore)
		{
			_mapper = mapper;
			_productService = productService;
			_tableStatusStore = tableStatusStore;
		}

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
			var tableStatus = (await _tableStatusStore.GetAllAsync()).ToList();
			_tableStatus = _mapper.Map<List<TableStatusDto>>(tableStatus);

			var brands = (await _productService.GetBrandsAsync())
				.Where(v => v.IsActive == true)
				.ToList();
			_brands = _mapper.Map<List<BrandDto>>(brands);
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

		public TableStatusDto GetTableStatusByCode(string statusCode)
			=> _tableStatus
				.Where(v => v.StatusCode.Equals(statusCode)).FirstOrDefault();

		public IReadOnlyList<BrandDto> GetBrands()
			=> _brands;
	}

}
