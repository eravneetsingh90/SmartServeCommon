using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Models
{
	public class TableStatusDto
	{
		public int Id { get; set; }

		public string StatusCode { get; set; } = null!;

		public string? StatusName { get; set; }

		public string? ColorHex { get; set; }

		public virtual ICollection<OrderDto> Orders { get; set; } = new List<OrderDto>();
	}
}
