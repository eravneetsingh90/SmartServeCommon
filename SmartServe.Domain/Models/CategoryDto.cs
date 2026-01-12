using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Models
{
	public class CategoryDto
	{
		public int CategoryId { get; set; }

		public string Name { get; set; } = null!;

		public bool? IsActive { get; set; }
		public bool? IsStock { get; set; }

		public int DisplayOrder { get; set; }

		public virtual ICollection<Product> Products { get; set; } = new List<Product>();
	}
}
