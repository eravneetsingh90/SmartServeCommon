using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class IngredientEntity
{
    public int IngredientId { get; set; }

    public string Name { get; set; } = null!;

    public string? Unit { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<ProductIngredientEntity> ProductIngredients { get; set; } = new List<ProductIngredientEntity>();
}
