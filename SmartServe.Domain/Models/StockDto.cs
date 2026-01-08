using SmartServe.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartServe.Domain.Models
{
	public class StockDto
	{
		public int Id { get; set; }

		public string ItemType { get; set; } = null!;

		public int ReferenceId { get; set; }

		public string Unit { get; set; } = null!;

		public decimal? MinStockLevel { get; set; }

		public bool? IsActive { get; set; }

		public DateTime? CreatedAt { get; set; }

		public virtual ICollection<StockTransactionDto> StockTransactions { get; set; } = new List<StockTransactionDto>();
	}

}
