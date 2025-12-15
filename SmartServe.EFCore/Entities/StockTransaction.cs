using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Entities;

public partial class StockTransaction
{
    public int StockTxnId { get; set; }

    public int? IngredientId { get; set; }

    public decimal ChangeQty { get; set; }

    public string? Reason { get; set; }

    public int? ReferenceId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Ingredient? Ingredient { get; set; }
}
