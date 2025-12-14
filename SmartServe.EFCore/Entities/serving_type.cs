using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Index("name", Name = "serving_types_name_key", IsUnique = true)]
public partial class serving_type
{
    [Key]
    public int serving_type_id { get; set; }

    [StringLength(50)]
    public string name { get; set; } = null!;

    //[InverseProperty("serving_type")]
    //public virtual ICollection<Product> products { get; set; } = new List<Product>();
}
