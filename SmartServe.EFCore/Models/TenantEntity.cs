using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class TenantEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Subdomain { get; set; }

    public int? OrderCounter { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<BrandEntity> Brands { get; set; } = new List<BrandEntity>();

    public virtual ICollection<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();

    public virtual ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();

    public virtual ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();

    public virtual ICollection<PaymentEntity> Payments { get; set; } = new List<PaymentEntity>();

    public virtual ICollection<ProductIngredientEntity> ProductIngredients { get; set; } = new List<ProductIngredientEntity>();

    public virtual ICollection<ProductVariantEntity> ProductVariants { get; set; } = new List<ProductVariantEntity>();

    public virtual ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();

    public virtual ICollection<RestaurantTableEntity> RestaurantTables { get; set; } = new List<RestaurantTableEntity>();

    public virtual ICollection<StockTransactionEntity> StockTransactions { get; set; } = new List<StockTransactionEntity>();

    public virtual ICollection<StockEntity> Stocks { get; set; } = new List<StockEntity>();

    public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}
