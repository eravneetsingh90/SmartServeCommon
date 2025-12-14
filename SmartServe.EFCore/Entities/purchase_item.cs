using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

public partial class purchase_item
{
    [Key]
    public int purchase_item_id { get; set; }

    public int? purchase_bill_id { get; set; }

    public int? ingredient_id { get; set; }

    [Precision(10, 2)]
    public decimal? quantity { get; set; }

    [Precision(10, 2)]
    public decimal? unit_price { get; set; }

    [Precision(10, 2)]
    public decimal? total_amount { get; set; }

    //[ForeignKey("ingredient_id")]
    //[InverseProperty("purchase_items")]
    //public virtual ingredient? ingredient { get; set; }

    //[ForeignKey("purchase_bill_id")]
    //[InverseProperty("purchase_items")]
    //public virtual purchase_bill? purchase_bill { get; set; }
}
