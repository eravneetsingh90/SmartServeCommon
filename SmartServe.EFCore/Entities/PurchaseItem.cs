using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class PurchaseItem
{
    public int PurchaseItemId { get; set; }

    public int? PurchaseBillId { get; set; }

    public int? IngredientId { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Ingredient? Ingredient { get; set; }

    public virtual PurchaseBill? PurchaseBill { get; set; }
}
