using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

[Table("one_time_tokens", Schema = "auth")]
public partial class OneTimeToken
{
    [Key]
    public Guid id { get; set; }

    public Guid user_id { get; set; }

    public string token_hash { get; set; } = null!;

    public string relates_to { get; set; } = null!;

    [Column(TypeName = "timestamp without time zone")]
    public DateTime created_at { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime updated_at { get; set; }

    //[ForeignKey("user_id")]
    //[InverseProperty("one_time_tokens")]
    //public virtual user user { get; set; } = null!;
}
