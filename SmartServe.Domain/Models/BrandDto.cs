namespace SmartServe.Domain.Models
{
	public class BrandDto
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public bool? IsActive { get; set; }

		public virtual ICollection<ProductVariantDto> ProductVariants { get; set; } = new List<ProductVariantDto>();
	}
}
