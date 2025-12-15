using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class PurchaseBill
{
    public int PurchaseBillId { get; set; }

    public int? VendorId { get; set; }

    public string? BillNumber { get; set; }

    public DateOnly? BillDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? ImageUrl { get; set; }

    public string? OcrStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<PurchaseBillParsedItem> PurchaseBillParsedItems { get; set; } = new List<PurchaseBillParsedItem>();

    public virtual ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();

    public virtual Vendor? Vendor { get; set; }
}
