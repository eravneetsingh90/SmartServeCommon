using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class Stock
{
    public int VariantId { get; set; }

    public int Quantity { get; set; }

    public virtual ProductVariant Variant { get; set; } = null!;
}
