using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.EFCore.Entities;

/// <summary>
/// Stores OAuth states for third-party provider authentication flows where Supabase acts as the OAuth client.
/// </summary>
[Table("oauth_client_states", Schema = "auth")]
[Index("created_at", Name = "idx_oauth_client_states_created_at")]
public partial class oauth_client_state
{
    [Key]
    public Guid id { get; set; }

    public string provider_type { get; set; } = null!;

    public string? code_verifier { get; set; }

    public DateTime created_at { get; set; }
}
