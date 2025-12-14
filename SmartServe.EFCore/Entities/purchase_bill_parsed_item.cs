using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

public partial class purchase_bill_parsed_item
{
    [Key]
    public int parsed_item_id { get; set; }

    public int? purchase_bill_id { get; set; }

    public string? raw_item_text { get; set; }

    [StringLength(200)]
    public string? extracted_name { get; set; }

    [Precision(10, 2)]
    public decimal? quantity { get; set; }

    [StringLength(20)]
    public string? unit { get; set; }

    [Precision(10, 2)]
    public decimal? unit_price { get; set; }

    [Precision(5, 2)]
    public decimal? confidence_score { get; set; }

    //[ForeignKey("purchase_bill_id")]
    //[InverseProperty("purchase_bill_parsed_items")]
    //public virtual purchase_bill? purchase_bill { get; set; }
}
