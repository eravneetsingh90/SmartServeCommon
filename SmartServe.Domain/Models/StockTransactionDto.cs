namespace SmartServe.Domain.Models
{
	public partial class StockTransactionDto
	{
		public int Id { get; set; }

		public int StockId { get; set; }

		public string TransactionType { get; set; } = null!;

		public decimal Quantity { get; set; }

		public string Reason { get; set; } = null!;

		public string? ReferenceType { get; set; }

		public DateTime? CreatedAt { get; set; }

		public virtual StockDto Stock { get; set; } = null!;
	}
}
