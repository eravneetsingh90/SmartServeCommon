namespace SmartServe.Domain.Models
{
	public class OrderItemDto
	{
		public int OrderItemId { get; set; }

		public int? OrderId { get; set; }

		public int? VariantId { get; set; }

		public int Quantity { get; set; }

		public decimal PriceSnapshot { get; set; }

		public decimal? DiscountAmount { get; set; }

		public virtual OrderDto? Order { get; set; }

		public virtual ProductVariantDto? Variant { get; set; }
	}
}
