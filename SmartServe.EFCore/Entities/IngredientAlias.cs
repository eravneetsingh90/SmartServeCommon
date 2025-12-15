using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class IngredientAlias
{
    public int AliasId { get; set; }

    public int? IngredientId { get; set; }

    public string AliasName { get; set; } = null!;

    public virtual Ingredient? Ingredient { get; set; }
}
