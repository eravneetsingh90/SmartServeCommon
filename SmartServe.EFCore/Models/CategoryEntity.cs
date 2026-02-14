using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class CategoryEntity
{
    public int Id { get; set; }

    public int TenantId { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }

    public int? DisplayOrder { get; set; }

    public virtual ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();

    public virtual TenantEntity Tenant { get; set; } = null!;
}
