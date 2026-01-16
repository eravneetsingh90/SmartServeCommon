using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Models
{
	public class ProductIngredientDto
	{
		public int ProductVariantId { get; set; }

		public int IngredientVariantId { get; set; }

		public decimal Quantity { get; set; }

		public DateTime? CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }

		public virtual ProductVariantDto IngredientVariant { get; set; } = null!;

		public virtual ProductVariantDto ProductVariant { get; set; } = null!;
	}
}
