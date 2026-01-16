namespace SmartServe.Domain.Models
{
	public class ProductDto
	{
		public int ProductId { get; set; }

		public string Name { get; set; } = null!;

		public int? CategoryId { get; set; }

		public bool? IsActive { get; set; }

		public DateTime? CreatedAt { get; set; }

		public int DisplayOrder { get; set; }

		public bool? IsStock { get; set; }

		public virtual CategoryDto? Category { get; set; }

		public virtual ICollection<ProductVariantDto> ProductVariants { get; set; } = new List<ProductVariantDto>();
	}
}
