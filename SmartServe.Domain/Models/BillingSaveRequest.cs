namespace SmartServe.Domain.Models
{
	public class BillingSaveRequest
	{
		public int? OrderId { get; set; } // null = new order
		public string OrderType { get; set; } // DINE_IN, DELIVERY, PICKUP
		public int? TableId { get; set; }

		public List<BillingItem> Items { get; set; } = new();

		public decimal DiscountValue { get; set; }
		public string DiscountType { get; set; } // FLAT, PERCENT
		public string DiscountReason { get; set; }
	}

}
