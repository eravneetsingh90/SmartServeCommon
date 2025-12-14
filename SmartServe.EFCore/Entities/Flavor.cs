using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Index("name", Name = "flavors_name_key", IsUnique = true)]
public partial class flavor
{
    [Key]
    public int flavor_id { get; set; }

    [StringLength(100)]
    public string name { get; set; } = null!;

    //[InverseProperty("flavor")]
    //public virtual ICollection<Product> products { get; set; } = new List<Product>();
}
