using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class StockEntity
{
    public int Id { get; set; }

    public string ItemType { get; set; } = null!;

    public int VariantId { get; set; }

    public string Unit { get; set; } = null!;

    public decimal? MinStockLevel { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<StockTransactionEntity> StockTransactions { get; set; } = new List<StockTransactionEntity>();

    public virtual ProductVariantEntity Variant { get; set; } = null!;
}
