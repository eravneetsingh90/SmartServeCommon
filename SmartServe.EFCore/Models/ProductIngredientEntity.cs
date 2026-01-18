using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class ProductIngredientEntity
{
    public int ProductVariantId { get; set; }

    public int IngredientVariantId { get; set; }

    public decimal Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ProductVariantEntity IngredientVariant { get; set; } = null!;

    public virtual ProductVariantEntity ProductVariant { get; set; } = null!;
}
