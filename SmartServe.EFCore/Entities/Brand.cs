using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartServe.EFCore.Entities;

[Index("name", Name = "brands_name_key", IsUnique = true)]
public class brand
{
    [Key]
    public int brand_id { get; set; }

    [StringLength(100)]
    public string name { get; set; } = null!;

    //[InverseProperty("brand")]
    //public virtual ICollection<Product> products { get; set; } = new List<Product>();
}
