using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IProductIngredientStore : IBaseStore<ProductIngredient>
	{
		Task<IEnumerable<ProductIngredient>> GetIngredientsForVariantAsync(int variantId);


	}
}
