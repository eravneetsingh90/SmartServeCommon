using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class StockTransaction
{
    public int StockTxnId { get; set; }

    public int StockItemId { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal Quantity { get; set; }

    public string Reason { get; set; } = null!;

    public string? ReferenceType { get; set; }

    public int? ReferenceId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual StockItem StockItem { get; set; } = null!;
}
