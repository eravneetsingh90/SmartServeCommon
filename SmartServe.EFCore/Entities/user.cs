using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Table("users")]
public partial class User
{
    [Key]
    public int user_id { get; set; }

    [StringLength(100)]
    public string name { get; set; }

    public int role_id { get; set; }

    public bool is_active { get; set; }

	public string pin_hash { get; set; }
	public DateTime? created_at { get; set; }

    //[InverseProperty("created_byNavigation")]
    //public virtual ICollection<order> orders { get; set; } = new List<order>();

    //[ForeignKey("role_id")]
    //[InverseProperty("user1s")]
    //public virtual role role { get; set; } = null!;
}
