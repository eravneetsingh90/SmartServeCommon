namespace SmartServe.Domain.Models
{
	public class AddStock
	{
		public string ItemType { get; set; }

		public int VariantId { get; set; }

		public string DisplayName { get; set; }

		public string Unit { get; set; }

		public decimal Quantity { get; set; }

		public string Reason { get; set; }
	}
}
