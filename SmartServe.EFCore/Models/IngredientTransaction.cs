using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class IngredientTransaction
{
    public int IngredientTxnId { get; set; }

    public int? IngredientId { get; set; }

    public decimal ChangeQty { get; set; }

    public string Reason { get; set; } = null!;

    public int? ReferenceId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Ingredient? Ingredient { get; set; }
}
