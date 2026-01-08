namespace SmartServe.Domain.Models
{
	public class AddStockDto
	{
		public string ItemType { get; set; }

		public int ReferenceId { get; set; }

		public string DisplayName { get; set; }

		public string Unit { get; set; }

		public decimal Quantity { get; set; }

		public string Reason { get; set; }
	}
}
