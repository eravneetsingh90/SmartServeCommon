using SmartServe.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartServe.Domain.Models
{
	public class Stock
	{
		public int Id { get; set; }

		public string ItemType { get; set; } = null!;

		public int VariantId { get; set; }

		public string Unit { get; set; } = null!;

		public decimal? MinStockLevel { get; set; }

		public bool? IsActive { get; set; }

		public DateTime? CreatedAt { get; set; }

		public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();

		public virtual ProductVariant Variant { get; set; } = null!;
	}

}
