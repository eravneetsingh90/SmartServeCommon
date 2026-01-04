using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class ProductIngredient
{
    public int VariantId { get; set; }

    public int IngredientId { get; set; }

    public decimal QtyRequired { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual ProductVariant Variant { get; set; } = null!;
}
