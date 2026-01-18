using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class OrderItemEntity
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? VariantId { get; set; }

    public int Quantity { get; set; }

    public decimal PriceSnapshot { get; set; }

    public decimal? DiscountAmount { get; set; }

    public virtual OrderEntity? Order { get; set; }

    public virtual ProductVariantEntity? Variant { get; set; }
}
