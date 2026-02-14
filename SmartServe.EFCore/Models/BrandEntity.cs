namespace SmartServe.EFCore.Models;

public partial class BrandEntity
{
    public int Id { get; set; }

    public int TenantId { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<ProductVariantEntity> ProductVariants { get; set; } = new List<ProductVariantEntity>();

    public virtual TenantEntity Tenant { get; set; } = null!;
}
