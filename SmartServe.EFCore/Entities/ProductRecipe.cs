using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[PrimaryKey("product_id", "ingredient_id")]
public partial class ProductRecipe
{
    [Key]
    public int product_id { get; set; }

    [Key]
    public int ingredient_id { get; set; }

    [Precision(10, 2)]
    public decimal qty_required { get; set; }

    //[ForeignKey("ingredient_id")]
    //[InverseProperty("product_recipes")]
    //public virtual ingredient ingredient { get; set; } = null!;

    //[ForeignKey("product_id")]
    //[InverseProperty("product_recipes")]
    public virtual Product product { get; set; } = null!;
}
