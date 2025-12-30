namespace SmartServe.Domain.Models
{
	public class RestaurantTableDto
	{
		public int TableId { get; set; }

		public string DisplayName { get; set; } = null!;

		public bool? IsActive { get; set; }

		public DateTime? CreatedAt { get; set; }

		public virtual ICollection<OrderDto> Orders { get; set; } = new List<OrderDto>();
	}
}
