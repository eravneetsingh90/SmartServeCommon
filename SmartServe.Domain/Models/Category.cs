using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Models
{
	public class Category
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public bool? IsActive { get; set; }

		public int DisplayOrder { get; set; }

		public virtual ICollection<Product> Products { get; set; } = new List<Product>();
	}
}
