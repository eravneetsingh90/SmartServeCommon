namespace SmartServe.Domain.Models
{
	public class ProductIngredientDto
	{
		public int VariantId { get; set; }

		public int IngredientId { get; set; }

		public decimal QtyRequired { get; set; }

		public virtual IngredientDto Ingredient { get; set; } = null!;

		public virtual ProductVariantDto Variant { get; set; } = null!;
	}
}
