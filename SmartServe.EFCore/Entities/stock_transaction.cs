using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Index("ingredient_id", Name = "idx_stock_txn_ingredient")]
public partial class stock_transaction
{
    [Key]
    public int stock_txn_id { get; set; }

    public int? ingredient_id { get; set; }

    [Precision(10, 2)]
    public decimal change_qty { get; set; }

    [StringLength(30)]
    public string? reason { get; set; }

    public int? reference_id { get; set; }

    public DateTime? created_at { get; set; }

    //[ForeignKey("ingredient_id")]
    //[InverseProperty("stock_transactions")]
    //public virtual ingredient? ingredient { get; set; }
}
