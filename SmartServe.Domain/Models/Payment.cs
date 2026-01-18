using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Models
{
	public class Payment
	{
		public int Id { get; set; }

		public int? OrderId { get; set; }

		public string? Mode { get; set; }

		public decimal? Amount { get; set; }

		public decimal? PartPaymentCash { get; set; }
		public string? Status { get; set; }

		public DateTime? CreatedAt { get; set; }

		public virtual Order? Order { get; set; }
	}
}
