using System;
using System.Collections.Generic;

namespace SmartServe.EFCore.Models;

public partial class RoleEntity
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}
