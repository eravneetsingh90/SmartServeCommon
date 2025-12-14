using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Table("kot")]
[Index("order_id", Name = "kot_order_id_key", IsUnique = true)]
public partial class kot
{
    [Key]
    public int kot_id { get; set; }

    public int? order_id { get; set; }

    [StringLength(20)]
    public string? status { get; set; }

    public bool? printed { get; set; }

    public DateTime? created_at { get; set; }

    //[ForeignKey("order_id")]
    //[InverseProperty("kot")]
    //public virtual order? order { get; set; }
}
