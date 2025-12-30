using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Models
{
	public class ProductVariantDto
	{
		public int ProductVariantId { get; set; }

		public int ProductId { get; set; }

		public int? BrandId { get; set; }

		public string Name { get; set; } = null!;

		public decimal Price { get; set; }

		public bool? TracksStock { get; set; }

		public bool? IsActive { get; set; }

		public DateTime? CreatedAt { get; set; }

		public int DisplayOrder { get; set; }

		public virtual Brand? Brand { get; set; }

		public virtual ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

		//public virtual ProductDto Product { get; set; } = null!;

		//public virtual ICollection<ProductIngredientDto> ProductIngredients { get; set; } = new List<ProductIngredientDto>();

		//public virtual StockDto? Stock { get; set; }

		//public virtual ICollection<StockTransactionDto> StockTransactions { get; set; } = new List<StockTransactionDto>();
	}
}
