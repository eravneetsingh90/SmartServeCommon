using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class ProductRecipe
{
    public int ProductId { get; set; }

    public int IngredientId { get; set; }

    public decimal QtyRequired { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
