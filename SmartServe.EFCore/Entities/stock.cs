using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class Stock
{
    public int IngredientId { get; set; }

    public decimal CurrentQty { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;
}
