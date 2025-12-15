using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class StockAdjustment
{
    public int AdjustmentId { get; set; }

    public int? IngredientId { get; set; }

    public decimal? ExpectedQty { get; set; }

    public decimal? ActualQty { get; set; }

    public decimal? VarianceQty { get; set; }

    public string? Reason { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Ingredient? Ingredient { get; set; }
}
