using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class Ingredient
{
    public int IngredientId { get; set; }

    public string Name { get; set; } = null!;

    public string? Unit { get; set; }

    public bool? IsActive { get; set; }

    public virtual IngredientStock? IngredientStock { get; set; }

    public virtual ICollection<IngredientTransaction> IngredientTransactions { get; set; } = new List<IngredientTransaction>();

    public virtual ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();
}
