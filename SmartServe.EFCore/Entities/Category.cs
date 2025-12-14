using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Index("name", Name = "categories_name_key", IsUnique = true)]
public partial class category
{
    [Key]
    public int category_id { get; set; }

    [StringLength(100)]
    public string name { get; set; } = null!;

    public bool? is_active { get; set; }

    //[InverseProperty("category")]
    //public virtual ICollection<Product> products { get; set; } = new List<Product>();
}
