using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Index("created_at", Name = "idx_orders_created_at")]
[Index("order_number", Name = "orders_order_number_key", IsUnique = true)]
public partial class order
{
    [Key]
    public int order_id { get; set; }

    public int? order_number { get; set; }

    [StringLength(20)]
    public string? order_type { get; set; }

    [StringLength(20)]
    public string? status { get; set; }

    [Precision(10, 2)]
    public decimal? total_amount { get; set; }

    public int? created_by { get; set; }

    public DateTime? created_at { get; set; }

    //[ForeignKey("created_by")]
    //[InverseProperty("orders")]
    //public virtual user1? created_byNavigation { get; set; }

    //[InverseProperty("order")]
    //public virtual kot? kot { get; set; }

    //[InverseProperty("order")]
    //public virtual ICollection<OrderItem> order_items { get; set; } = new List<OrderItem>();

    //[InverseProperty("order")]
    //public virtual ICollection<payment> payments { get; set; } = new List<payment>();
}
