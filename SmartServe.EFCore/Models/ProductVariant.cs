using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class ProductVariant
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int? BrandId { get; set; }

    public string VariantName { get; set; } = null!;

    public decimal Price { get; set; }

    public bool? IsActive { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductIngredient> ProductIngredientIngredientVariants { get; set; } = new List<ProductIngredient>();

    public virtual ICollection<ProductIngredient> ProductIngredientProductVariants { get; set; } = new List<ProductIngredient>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
