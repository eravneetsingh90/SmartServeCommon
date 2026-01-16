namespace SmartServe.EFCore.Models;
public partial class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public int? CategoryId { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int DisplayOrder { get; set; }
    public virtual Category? Category { get; set; }
    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
