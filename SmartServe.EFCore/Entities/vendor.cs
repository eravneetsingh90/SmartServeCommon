using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? GstNo { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<PurchaseBill> PurchaseBills { get; set; } = new List<PurchaseBill>();
}
