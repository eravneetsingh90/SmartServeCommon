using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartServe.EFCore.Entities;

[Index("category_id", Name = "idx_products_category")]
[Index("flavor_id", Name = "idx_products_flavor")]
public class Product
{
    [Key]
    public int product_id { get; set; }

    [StringLength(150)]
    public string name { get; set; } = null!;

    public int category_id { get; set; }

    public int? flavor_id { get; set; }

    public int serving_type_id { get; set; }

    public int? brand_id { get; set; }

    [Precision(10, 2)]
    public decimal price { get; set; }

    public bool? is_active { get; set; }

    public DateTime? created_at { get; set; }

    //[ForeignKey("brand_id")]
    //[InverseProperty("products")]
    //public virtual brand? brand { get; set; }

    //[ForeignKey("category_id")]
    //[InverseProperty("products")]
    //public virtual category category { get; set; } = null!;

    //[ForeignKey("flavor_id")]
    //[InverseProperty("products")]
    //public virtual flavor? flavor { get; set; }

    //[InverseProperty("product")]
    //public virtual ICollection<OrderItem> order_items { get; set; } = new List<OrderItem>();

    //[InverseProperty("product")]
    //public virtual ICollection<ProductRecipe> product_recipes { get; set; } = new List<ProductRecipe>();

    //[ForeignKey("serving_type_id")]
    //[InverseProperty("products")]
    //public virtual serving_type serving_type { get; set; } = null!;
}
