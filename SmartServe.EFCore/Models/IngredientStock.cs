using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class IngredientStock
{
    public int IngredientId { get; set; }

    public decimal? Quantity { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;
}
