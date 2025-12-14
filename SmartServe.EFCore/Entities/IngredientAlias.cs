using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

public partial class IngredientAlias
{
    [Key]
    public int alias_id { get; set; }

    public int? ingredient_id { get; set; }

    [StringLength(150)]
    public string alias_name { get; set; } = null!;

    //[ForeignKey("ingredient_id")]
    //[InverseProperty("ingredient_aliases")]
    //public virtual ingredient? ingredient { get; set; }
}
