using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class StockTransaction
{
    public int StockTxnId { get; set; }

    public int? VariantId { get; set; }

    public int ChangeQty { get; set; }

    public string Reason { get; set; } = null!;

    public int? ReferenceId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ProductVariant? Variant { get; set; }
}
