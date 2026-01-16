namespace SmartServe.EFCore.Models;
public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }

    public int DisplayOrder { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
