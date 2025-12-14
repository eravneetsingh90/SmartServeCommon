using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Index("name", Name = "ingredients_name_key", IsUnique = true)]
public partial class ingredient
{
    [Key]
    public int ingredient_id { get; set; }

    [StringLength(150)]
    public string name { get; set; } = null!;

    [StringLength(20)]
    public string base_unit { get; set; } = null!;

    [Precision(5, 2)]
    public decimal? ideal_variance_percent { get; set; }

    public bool? is_active { get; set; }

    //[InverseProperty("ingredient")]
    //public virtual ICollection<IngredientAlias> ingredient_aliases { get; set; } = new List<IngredientAlias>();

    //[InverseProperty("ingredient")]
    //public virtual ICollection<ProductRecipe> product_recipes { get; set; } = new List<ProductRecipe>();

    //[InverseProperty("ingredient")]
    //public virtual ICollection<purchase_item> purchase_items { get; set; } = new List<purchase_item>();

    //[InverseProperty("ingredient")]
    //public virtual stock? stock { get; set; }

    //[InverseProperty("ingredient")]
    //public virtual ICollection<stock_adjustment> stock_adjustments { get; set; } = new List<stock_adjustment>();

    //[InverseProperty("ingredient")]
    //public virtual ICollection<stock_transaction> stock_transactions { get; set; } = new List<stock_transaction>();
}
