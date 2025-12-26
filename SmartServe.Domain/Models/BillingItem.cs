namespace SmartServe.Domain.Models
{
	public class BillingItem
	{
		public int VariantId { get; set; }
		public int Quantity { get; set; }
		public decimal PriceSnapshot { get; set; }
	}

}
