using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class PurchaseBillParsedItem
{
    public int ParsedItemId { get; set; }

    public int? PurchaseBillId { get; set; }

    public string? RawItemText { get; set; }

    public string? ExtractedName { get; set; }

    public decimal? Quantity { get; set; }

    public string? Unit { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? ConfidenceScore { get; set; }

    public virtual PurchaseBill? PurchaseBill { get; set; }
}
