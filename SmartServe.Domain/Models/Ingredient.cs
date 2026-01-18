using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Models
{
	public class Ingredient
	{
		public int IngredientId { get; set; }

		public string Name { get; set; } = null!;

		public string? Unit { get; set; }

		public bool? IsActive { get; set; }

		public virtual ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();
	}
}
