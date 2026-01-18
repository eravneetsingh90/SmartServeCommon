using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class ProductVariantEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int? BrandId { get; set; }

    public string VariantName { get; set; } = null!;

    public decimal Price { get; set; }

    public bool? IsActive { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual BrandEntity? Brand { get; set; }

    public virtual ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();

    public virtual ProductEntity Product { get; set; } = null!;

    public virtual ICollection<ProductIngredientEntity> ProductIngredientIngredientVariants { get; set; } = new List<ProductIngredientEntity>();

    public virtual ICollection<ProductIngredientEntity> ProductIngredientProductVariants { get; set; } = new List<ProductIngredientEntity>();

    public virtual ICollection<StockEntity> Stocks { get; set; } = new List<StockEntity>();
}
