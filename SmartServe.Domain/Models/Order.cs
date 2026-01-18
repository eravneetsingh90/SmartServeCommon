using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Models
{
	public class Order
	{
		public int Id { get; set; }

		public string? OrderNumber { get; set; }

		public string? OrderType { get; set; }

		public int? TableId { get; set; }

		public int? StatusId { get; set; }

		public decimal? TotalAmount { get; set; }

		public string? DiscountType { get; set; }

		public decimal? DiscountValue { get; set; }

		public string? DiscountReason { get; set; }

		public DateTime? CreatedAt { get; set; }

		public DateTime? ClosedAt { get; set; }

		public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

		public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

		public virtual TableStatus? Status { get; set; }

		public virtual RestaurantTable? Table { get; set; }

	}

}
