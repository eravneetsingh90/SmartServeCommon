using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IIngredientStore
	{
		Task<IEnumerable<ProductIngredient>> GetIngredientsForVariantAsync(int variantId);

		Task UpdateIngredientStockAsync(int ingredientId, decimal newQty);
		Task AddIngredientTransactionAsync(IngredientTransaction txn);
	}

}
