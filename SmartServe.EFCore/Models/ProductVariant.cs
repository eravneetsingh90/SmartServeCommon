using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class ProductVariant
{
    public int ProductVariantId { get; set; }

    public int ProductId { get; set; }

    public int? BrandId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public bool? TracksStock { get; set; }
	public string StockMode { get; set; }

	public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
    
    public int DisplayOrder { get; set; }

	public virtual Brand? Brand { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();

    public virtual Stock? Stock { get; set; }

    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}
