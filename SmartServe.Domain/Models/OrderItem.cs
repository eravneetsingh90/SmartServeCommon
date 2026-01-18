using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Models
{
	public class OrderItem
	{
		public int Id { get; set; }

		public int? OrderId { get; set; }

		public int? VariantId { get; set; }

		public int Quantity { get; set; }

		public decimal PriceSnapshot { get; set; }

		public decimal? DiscountAmount { get; set; }

		public virtual Order? Order { get; set; }

		public virtual ProductVariant? Variant { get; set; }
	}
}
