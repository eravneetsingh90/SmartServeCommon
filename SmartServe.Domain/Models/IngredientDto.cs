using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Models
{
	public class IngredientDto
	{
		public int IngredientId { get; set; }

		public string Name { get; set; } = null!;

		public string? Unit { get; set; }

		public bool? IsActive { get; set; }

		public virtual ICollection<ProductIngredientDto> ProductIngredients { get; set; } = new List<ProductIngredientDto>();
	}
}
