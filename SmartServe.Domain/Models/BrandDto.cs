namespace SmartServe.Domain.Models
{
	public class BrandDto
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public bool? IsActive { get; set; }

		public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
	}
}
