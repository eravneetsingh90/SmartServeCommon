using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Table("stock")]
public partial class stock
{
    [Key]
    public int ingredient_id { get; set; }

    [Precision(12, 2)]
    public decimal current_qty { get; set; }

    //[ForeignKey("ingredient_id")]
    //[InverseProperty("stock")]
    //public virtual ingredient ingredient { get; set; } = null!;
}
