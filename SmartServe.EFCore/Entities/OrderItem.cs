using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Index("order_id", Name = "idx_order_items_order_id")]
[Index("product_id", Name = "idx_order_items_product_id")]
public partial class OrderItem
{
    [Key]
    public int order_item_id { get; set; }

    public int order_id { get; set; }

    public int product_id { get; set; }

    public int quantity { get; set; }

    [Precision(10, 2)]
    public decimal price_snapshot { get; set; }

    public string? notes { get; set; }

    //[ForeignKey("order_id")]
    //[InverseProperty("order_items")]
    //public virtual order order { get; set; } = null!;

    //[ForeignKey("product_id")]
    //[InverseProperty("order_items")]
    //public virtual Product product { get; set; } = null!;
}
