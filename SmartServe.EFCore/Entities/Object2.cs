using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Table("objects", Schema = "storage")]
[Index("bucket_id", "name", Name = "bucketid_objname", IsUnique = true)]
public partial class object2
{
    [Key]
    public Guid id { get; set; }

    public string? bucket_id { get; set; }

    public string? name { get; set; }

    /// <summary>
    /// Field is deprecated, use owner_id instead
    /// </summary>
    public Guid? owner { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public DateTime? last_accessed_at { get; set; }

    [Column(TypeName = "jsonb")]
    public string? metadata { get; set; }

    public List<string>? path_tokens { get; set; }

    public string? version { get; set; }

    public string? owner_id { get; set; }

    [Column(TypeName = "jsonb")]
    public string? user_metadata { get; set; }

    public int? level { get; set; }

    //[ForeignKey("bucket_id")]
    //[InverseProperty("objects")]
    //public virtual bucket? bucket { get; set; }
}
