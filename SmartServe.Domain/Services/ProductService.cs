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
		private readonly IBrandStore _brandStore;
		private readonly IStockStore _stockStore;
		#endregion

		#region constructor
		public ProductService(
			IMapper mapper,
			ICategoryStore categoryStore,
			IProductStore productStore,
			IProductVariantStore variantStore,
			IStockService stockService,
			IBrandStore brandStore,
			IStockStore stockStore)
		{
			_mapper = mapper;
			_categoryStore = categoryStore;
			_productStore = productStore;
			_variantStore = variantStore;
			_brandStore = brandStore;
			_stockStore = stockStore;
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
		public async Task<List<ProductVariantDto>> GetVariantsByBrandIdAsync(int brandId)
		{
			var items = await _variantStore.GetByBrandIdAsync(brandId);
			return _mapper.Map<List<ProductVariantDto>>(items);
		}
		public async Task<List<BrandDto>> GetBrandsAsync()
		{
			var items = await _brandStore.GetAllAsync();
			return _mapper.Map<List<BrandDto>>(items);
		}
		public async Task<List<StockDto>> GetAllStockAsync()
		{
			var items = await _stockStore.GetAllStockAsync();
			return _mapper.Map<List<StockDto>>(items);
		}
		public async Task<BaseResponse> DeleteCategoryAsync(int productVariantId)
		{
			var response = BaseResponse.New();
			try
			{
				await _categoryStore.DeleteAsync(productVariantId);
			}
			catch (Exception ex)
			{
				response.MetaData.ResultCode = ResultCodes.Error;
				response.MetaData.ResultMessage = ResultMessages.Error;
			}
			return response;
		}
		public async Task<BaseResponse> DeleteProductAsync(int productVariantId)
		{
			var response = BaseResponse.New();
			try
			{
				await _productStore.DeleteAsync(productVariantId);
			}
			catch (Exception ex)
			{
				response.MetaData.ResultCode = ResultCodes.Error;
				response.MetaData.ResultMessage = ResultMessages.Error;
			}
			return response;
		}
		public async Task<BaseResponse> DeleteVariantAsync(int productVariantId)
		{
			var response = BaseResponse.New();
			try
			{
				await _variantStore.DeleteAsync(productVariantId);
			}
			catch (Exception ex)
			{
				response.MetaData.ResultCode = ResultCodes.Error;
				response.MetaData.ResultMessage = ResultMessages.Error;
			}
			return response;
		}
		public async Task<BaseResponse> SaveBulkCategoriesAsync(IEnumerable<CategoryDto> items)
		{
			var response = BaseResponse.New();
			try
			{
				await _categoryStore.SaveBulkAsync(_mapper.Map<List<Category>>(items));
			}
			catch (DuplicateWaitObjectException ex)
			{
				response.MetaData.ResultCode = ResultCodes.DuplicateNotAllowed;
				response.MetaData.ResultMessage = "Duplicate category names are not allowed.";
			}
			catch (Exception ex)
			{
				response.MetaData.ResultCode = ResultCodes.Error;
				response.MetaData.ResultMessage = ResultMessages.Error;
			}
			return response;
		}
		public async Task<BaseResponse> SaveBulkProductsAsync(IEnumerable<ProductDto> items)
		{
			var response = BaseResponse.New();
			try
			{
				await _productStore.SaveBulkAsync(_mapper.Map<List<Product>>(items));
			}
			catch (DuplicateWaitObjectException ex)
			{
				response.MetaData.ResultCode = ResultCodes.DuplicateNotAllowed;
				response.MetaData.ResultMessage = "Duplicate product names are not allowed within the same category.";
			}
			catch (Exception ex)
			{
				response.MetaData.ResultCode = ResultCodes.Error;
				response.MetaData.ResultMessage = ResultMessages.Error;
			}
			return response;
		}
		public async Task<BaseResponse> SaveBulkVariantAsync(IEnumerable<ProductVariantDto> productVariants)
		{
			var response = BaseResponse.New();
			try
			{
				await _variantStore.SaveBulkAsync(_mapper.Map<List<ProductVariant>>(productVariants));
			}
			catch (DuplicateWaitObjectException ex)
			{
				response.MetaData.ResultCode = ResultCodes.DuplicateNotAllowed;
				response.MetaData.ResultMessage = "Duplicate variant names are not allowed for the same product.";
			}
			catch (Exception ex)
			{
				response.MetaData.ResultCode = ResultCodes.Error;
				response.MetaData.ResultMessage = ResultMessages.Error;
			}
			return response;
		}

	}
}
