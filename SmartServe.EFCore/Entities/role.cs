using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Index("role_name", Name = "roles_role_name_key", IsUnique = true)]
public partial class role
{
    [Key]
    public int role_id { get; set; }

    [StringLength(50)]
    public string role_name { get; set; } = null!;

    //[InverseProperty("role")]
    //public virtual ICollection<user1> user1s { get; set; } = new List<user1>();
}
