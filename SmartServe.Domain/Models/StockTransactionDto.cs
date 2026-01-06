using SmartServe.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartServe.Domain.Models
{
	public partial class StockTransactionDto
	{
		public int StockTxnId { get; set; }

		public int StockItemId { get; set; }

		public string TransactionType { get; set; } = null!;

		public decimal Quantity { get; set; }

		public string Reason { get; set; } = null!;

		public string? ReferenceType { get; set; }

		public int? ReferenceId { get; set; }

		public DateTime? CreatedAt { get; set; }

		public virtual StockItemDto StockItem { get; set; } = null!;
	}
}
