using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

public partial class payment
{
    [Key]
    public int payment_id { get; set; }

    public int? order_id { get; set; }

    [StringLength(20)]
    public string? mode { get; set; }

    [Precision(10, 2)]
    public decimal? amount { get; set; }

    [StringLength(20)]
    public string? status { get; set; }

    public DateTime? created_at { get; set; }

    //[ForeignKey("order_id")]
    //[InverseProperty("payments")]
    //public virtual order? order { get; set; }
}
