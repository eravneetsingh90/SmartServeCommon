using AutoMapper;
using SmartServe.Domain.Constants;
using SmartServe.Domain.Models;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class ProductService : IProductService
	{
		#region fields
		private readonly IMapper _mapper;
		private readonly ICategoryStore _categoryStore;
		private readonly IProductStore _productStore;
		private readonly IProductVariantStore _variantStore;
		#endregion

		#region constructor
		public ProductService(
			IMapper mapper,
			ICategoryStore categoryStore,
			IProductStore productStore,
			IProductVariantStore variantStore,
			IStockService stockService)
		{
			_mapper = mapper;
			_categoryStore = categoryStore;
			_productStore = productStore;
			_variantStore = variantStore;
		}
		#endregion

		public async Task<List<CategoryDto>> GetCategoriesAsync()
		{
			var items = await _categoryStore.GetAllAsync();
			return _mapper.Map<List<CategoryDto>>(items);
		}
		public async Task<List<ProductDto>> GetProductsAsync()
		{
			var items = await _productStore.GetAllAsync();
			return _mapper.Map<List<ProductDto>>(items);
		}
		public async Task<List<ProductVariantDto>> GetProductVariantsAsync()
		{
			var items = await _variantStore.GetAllAsync();
			return _mapper.Map<List<ProductVariantDto>>(items);
		}
		public async Task<List<ProductDto>> GetProductByCategoryIdAsync(int categoryId)
		{
			var items = await _productStore.GetByCategoryIdAsync(categoryId);
			return _mapper.Map<List<ProductDto>>(items);
		}

		public async Task<List<ProductVariantDto>> GetVariantByProductIdAsync(int productId)
		{
			var items = await _variantStore.GetByProductIdAsync(productId);
			return _mapper.Map<List<ProductVariantDto>>(items);
		}

		public async Task DeleteVariantAsync(int productVariantId)
		{
			_ = _variantStore.DeleteAsync(productVariantId);
		}

		public async Task SaveBulkVariantAsync(IEnumerable<ProductVariantDto> productVariants)
		{
			await _variantStore.SaveBulkAsync(_mapper.Map<List<ProductVariant>>(productVariants));
		}
	}
}
