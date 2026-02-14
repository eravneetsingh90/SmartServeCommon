using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class RestaurantTableEntity
{
    public int Id { get; set; }

    public int TenantId { get; set; }

    public string DisplayName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();

    public virtual TenantEntity Tenant { get; set; } = null!;
}
