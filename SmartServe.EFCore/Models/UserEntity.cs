using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class UserEntity 
{
    public int Id { get; set; }

    public int TenantId { get; set; }

    public string Name { get; set; } = null!;

    public int? RoleId { get; set; }

    public string? PinHash { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual RoleEntity? Role { get; set; }

    public virtual TenantEntity Tenant { get; set; } = null!;
}
