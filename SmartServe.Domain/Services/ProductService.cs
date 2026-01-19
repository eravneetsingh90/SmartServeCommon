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
		public async Task<List<Category>> GetCategoriesAsync()
		{
			var items = await _categoryStore.GetAllAsync();
			return _mapper.Map<List<Category>>(items);
		}
		public async Task<List<Category>> GetActiveCategoriesAsync()
		{
			var items = (await _categoryStore.GetAllAsync())
				.Where(c => c.IsActive == true)
				.OrderBy(c => c.DisplayOrder)
				.ToList();
			return _mapper.Map<List<Category>>(items);
		}

		public async Task<List<Product>> GetActiveProductsAsync()
		{
			var items = (await _productStore.GetAllAsync())
				.Where(c => c.IsActive == true)
				.OrderBy(c => c.DisplayOrder)
				.ToList();
			return _mapper.Map<List<Product>>(items);
		}

		public async Task<List<ProductVariant>> GetActiveProductVariantsAsync()
		{
			var items = (await _variantStore.GetAllAsync())
				.Where(c => c.IsActive == true)
				.OrderBy(c => c.DisplayOrder)
				.ToList();
			return _mapper.Map<List<ProductVariant>>(items);
		}
		public async Task<List<Product>> GetProductsAsync()
		{
			var items = await _productStore.GetAllAsync();
			return _mapper.Map<List<Product>>(items);
		}
		public async Task<List<ProductVariant>> GetProductVariantsAsync()
		{
			var items = await _variantStore.GetAllAsync();
			return _mapper.Map<List<ProductVariant>>(items);
		}
		public async Task<List<Product>> GetProductByCategoryIdAsync(int categoryId)
		{
			var items = await _productStore.GetByCategoryIdAsync(categoryId);
			return _mapper.Map<List<Product>>(items);
		}
		public async Task<List<ProductVariant>> GetVariantByProductIdAsync(int productId)
		{
			var items = await _variantStore.GetByProductIdAsync(productId);
			return _mapper.Map<List<ProductVariant>>(items);
		}
		public async Task<List<ProductVariant>> GetVariantsByBrandIdAsync(int brandId)
		{
			var items = await _variantStore.GetByBrandIdAsync(brandId);
			return _mapper.Map<List<ProductVariant>>(items);
		}
		public async Task<List<Brand>> GetBrandsAsync()
		{
			var items = await _brandStore.GetAllAsync();
			return _mapper.Map<List<Brand>>(items);
		}
		public async Task<List<Stock>> GetAllStockAsync()
		{
			var items = await _stockStore.GetAllStockAsync();
			return _mapper.Map<List<Stock>>(items);
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
		public async Task<BaseResponse> SaveBulkCategoriesAsync(IEnumerable<Category> items)
		{
			var response = BaseResponse.New();
			try
			{
				await _categoryStore.SaveBulkAsync(_mapper.Map<List<CategoryEntity>>(items));
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
		public async Task<BaseResponse> SaveBulkProductsAsync(IEnumerable<Product> items)
		{
			var response = BaseResponse.New();
			try
			{
				await _productStore.SaveBulkAsync(_mapper.Map<List<ProductEntity>>(items));
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
		public async Task<BaseResponse> SaveBulkVariantAsync(IEnumerable<ProductVariant> productVariants)
		{
			var response = BaseResponse.New();
			try
			{
				await _variantStore.SaveBulkAsync(_mapper.Map<List<ProductVariantEntity>>(productVariants));
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
