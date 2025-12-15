using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class Ingredient
{
    public int IngredientId { get; set; }

    public string Name { get; set; } = null!;

    public string BaseUnit { get; set; } = null!;

    public decimal? IdealVariancePercent { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<IngredientAlias> IngredientAliases { get; set; } = new List<IngredientAlias>();

    public virtual ICollection<ProductRecipe> ProductRecipes { get; set; } = new List<ProductRecipe>();

    public virtual ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();

    public virtual Stock? Stock { get; set; }

    public virtual ICollection<StockAdjustment> StockAdjustments { get; set; } = new List<StockAdjustment>();

    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}
