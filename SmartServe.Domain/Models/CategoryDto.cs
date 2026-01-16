using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Models
{
	public class CategoryDto
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public bool? IsActive { get; set; }

		public int DisplayOrder { get; set; }

		public virtual ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
	}
}
