using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Models
{
	public class OrderDto
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

		public virtual ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

		public virtual ICollection<PaymentDto> Payments { get; set; } = new List<PaymentDto>();

		public virtual TableStatusDto? Status { get; set; }

		public virtual RestaurantTableDto? Table { get; set; }

	}

}
