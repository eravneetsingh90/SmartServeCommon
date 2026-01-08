namespace SmartServe.Domain.Models
{
	public class CurrentStockDto
	{
		public int Id { get; set; }

		public string ItemType { get; set; }
		public int ReferenceId { get; set; }

		public string ItemName { get; set; }   // Filled via JOIN later
		public string Unit { get; set; }

		public decimal CurrentQuantity { get; set; }
		public decimal MinStockLevel { get; set; }

		public bool IsLowStock => CurrentQuantity <= MinStockLevel;
	}

}
