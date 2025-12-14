using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

public partial class stock_adjustment
{
    [Key]
    public int adjustment_id { get; set; }

    public int? ingredient_id { get; set; }

    [Precision(10, 2)]
    public decimal? expected_qty { get; set; }

    [Precision(10, 2)]
    public decimal? actual_qty { get; set; }

    [Precision(10, 2)]
    public decimal? variance_qty { get; set; }

    [StringLength(50)]
    public string? reason { get; set; }

    public DateTime? created_at { get; set; }

    //[ForeignKey("ingredient_id")]
    //[InverseProperty("stock_adjustments")]
    //public virtual ingredient? ingredient { get; set; }
}
