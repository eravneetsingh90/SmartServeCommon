using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class Stock
{
    public int Id { get; set; }

    public string ItemType { get; set; } = null!;

    public int ReferenceId { get; set; }

    public string Unit { get; set; } = null!;

    public decimal? MinStockLevel { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}
