using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class ProductIngredient
{
    public int ProductVariantId { get; set; }

    public int IngredientVariantId { get; set; }

    public decimal Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ProductVariant IngredientVariant { get; set; } = null!;

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
