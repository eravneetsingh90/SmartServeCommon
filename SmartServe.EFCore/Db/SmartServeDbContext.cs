using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Entities;

namespace SmartServe.EFCore.Db;

public partial class SmartServeDbContext : DbContext
{
    public SmartServeDbContext()
    {
    }

    public SmartServeDbContext(DbContextOptions<SmartServeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLogEntry> audit_log_entries { get; set; }

    public virtual DbSet<brand> brands { get; set; }

    public virtual DbSet<bucket> buckets { get; set; }

    public virtual DbSet<BucketsAnalytic> buckets_analytics { get; set; }

    public virtual DbSet<buckets_vector> buckets_vectors { get; set; }

    public virtual DbSet<category> categories { get; set; }

    public virtual DbSet<flavor> flavors { get; set; }

    public virtual DbSet<FlowState> flow_states { get; set; }

    public virtual DbSet<identity> identities { get; set; }

    public virtual DbSet<ingredient> ingredients { get; set; }

    public virtual DbSet<IngredientAlias> ingredient_aliases { get; set; }

    public virtual DbSet<instance> instances { get; set; }

    public virtual DbSet<kot> kots { get; set; }

    public virtual DbSet<MfaAmrClaim> mfa_amr_claims { get; set; }

    public virtual DbSet<MFAChallenge> mfa_challenges { get; set; }

    public virtual DbSet<mfa_factor> mfa_factors { get; set; }

    public virtual DbSet<migration> migrations { get; set; }

    public virtual DbSet<OauthAuthorization> oauth_authorizations { get; set; }

    public virtual DbSet<oauth_client> oauth_clients { get; set; }

    public virtual DbSet<oauth_client_state> oauth_client_states { get; set; }

    public virtual DbSet<OauthConsent> oauth_consents { get; set; }

    public virtual DbSet<object2> objects { get; set; }

    public virtual DbSet<OneTimeToken> one_time_tokens { get; set; }

    public virtual DbSet<order> orders { get; set; }

    public virtual DbSet<OrderItem> order_items { get; set; }

    public virtual DbSet<payment> payments { get; set; }

    public virtual DbSet<prefix> prefixes { get; set; }

    public virtual DbSet<Product> products { get; set; }

    public virtual DbSet<ProductRecipe> product_recipes { get; set; }

    public virtual DbSet<purchase_bill> purchase_bills { get; set; }

    public virtual DbSet<purchase_bill_parsed_item> purchase_bill_parsed_items { get; set; }

    public virtual DbSet<purchase_item> purchase_items { get; set; }

    public virtual DbSet<refresh_token> refresh_tokens { get; set; }

    public virtual DbSet<role> roles { get; set; }

    public virtual DbSet<s3_multipart_upload> s3_multipart_uploads { get; set; }

    public virtual DbSet<s3_multipart_uploads_part> s3_multipart_uploads_parts { get; set; }

    public virtual DbSet<saml_provider> saml_providers { get; set; }

    public virtual DbSet<saml_relay_state> saml_relay_states { get; set; }

    public virtual DbSet<schema_migration> schema_migrations { get; set; }

    public virtual DbSet<schema_migration1> schema_migrations1 { get; set; }

    public virtual DbSet<serving_type> serving_types { get; set; }

    public virtual DbSet<session> sessions { get; set; }

    public virtual DbSet<sso_domain> sso_domains { get; set; }

    public virtual DbSet<sso_provider> sso_providers { get; set; }

    public virtual DbSet<stock> stocks { get; set; }

    public virtual DbSet<stock_adjustment> stock_adjustments { get; set; }

    public virtual DbSet<stock_transaction> stock_transactions { get; set; }

    public virtual DbSet<subscription> subscriptions { get; set; }

    public virtual DbSet<unit_conversion> unit_conversions { get; set; }

    public virtual DbSet<user> users { get; set; }

    public virtual DbSet<user1> users1 { get; set; }

    public virtual DbSet<vector_index> vector_indexes { get; set; }

    public virtual DbSet<vendor> vendors { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseNpgsql("Host=aws-1-ap-south-1.pooler.supabase.com;Database=postgres;Username=postgres.wuadxvnovhplslorqwng;Password=Golumolu@1990;SSL Mode=Require;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.Entity<Product>(entity =>
		{
			entity.HasKey(e => e.product_id).HasName("products_pkey");

		});
	}
	/*
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "oauth_authorization_status", new[] { "pending", "approved", "denied", "expired" })
            .HasPostgresEnum("auth", "oauth_client_type", new[] { "public", "confidential" })
            .HasPostgresEnum("auth", "oauth_registration_type", new[] { "dynamic", "manual" })
            .HasPostgresEnum("auth", "oauth_response_type", new[] { "code" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresEnum("storage", "buckettype", new[] { "STANDARD", "ANALYTICS", "VECTOR" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<AuditLogEntry>(entity =>
        {
            entity.HasKey(e => e.id).HasName("audit_log_entries_pkey");

            entity.ToTable("audit_log_entries", "auth", tb => tb.HasComment("Auth: Audit trail for user actions."));

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.ip_address).HasDefaultValueSql("''::character varying");
        });

        modelBuilder.Entity<brand>(entity =>
        {
            entity.HasKey(e => e.brand_id).HasName("brands_pkey");
        });

        modelBuilder.Entity<bucket>(entity =>
        {
            entity.HasKey(e => e.id).HasName("buckets_pkey");

            entity.Property(e => e._public).HasDefaultValue(false);
            entity.Property(e => e.avif_autodetection).HasDefaultValue(false);
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.owner).HasComment("Field is deprecated, use owner_id instead");
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<BucketsAnalytic>(entity =>
        {
            entity.HasKey(e => e.id).HasName("buckets_analytics_pkey");

            entity.HasIndex(e => e.name, "buckets_analytics_unique_name_idx")
                .IsUnique()
                .HasFilter("(deleted_at IS NULL)");

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.format).HasDefaultValueSql("'ICEBERG'::text");
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<buckets_vector>(entity =>
        {
            entity.HasKey(e => e.id).HasName("buckets_vectors_pkey");

            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<category>(entity =>
        {
            entity.HasKey(e => e.category_id).HasName("categories_pkey");

            entity.Property(e => e.is_active).HasDefaultValue(true);
        });

        modelBuilder.Entity<flavor>(entity =>
        {
            entity.HasKey(e => e.flavor_id).HasName("flavors_pkey");
        });

        modelBuilder.Entity<FlowState>(entity =>
        {
            entity.HasKey(e => e.id).HasName("flow_state_pkey");

            entity.ToTable("flow_state", "auth", tb => tb.HasComment("stores metadata for pkce logins"));

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<identity>(entity =>
        {
            entity.HasKey(e => e.id).HasName("identities_pkey");

            entity.ToTable("identities", "auth", tb => tb.HasComment("Auth: Stores identities associated to a user."));

            entity.HasIndex(e => e.email, "identities_email_idx").HasOperators(new[] { "text_pattern_ops" });

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.email)
                .HasComputedColumnSql("lower((identity_data ->> 'email'::text))", true)
                .HasComment("Auth: Email is a generated column that references the optional email property in the identity_data");

            //entity.HasOne(d => d.user).WithMany(p => p.identities).HasConstraintName("identities_user_id_fkey");
        });

        modelBuilder.Entity<ingredient>(entity =>
        {
            entity.HasKey(e => e.ingredient_id).HasName("ingredients_pkey");

            entity.Property(e => e.ideal_variance_percent).HasDefaultValueSql("8.0");
            entity.Property(e => e.is_active).HasDefaultValue(true);
        });

        modelBuilder.Entity<IngredientAlias>(entity =>
        {
            entity.HasKey(e => e.alias_id).HasName("ingredient_aliases_pkey");

            //entity.HasOne(d => d.ingredient).WithMany(p => p.ingredient_aliases)
            //    .OnDelete(DeleteBehavior.Cascade)
            //    .HasConstraintName("ingredient_aliases_ingredient_id_fkey");
        });

        modelBuilder.Entity<instance>(entity =>
        {
            entity.HasKey(e => e.id).HasName("instances_pkey");

            entity.ToTable("instances", "auth", tb => tb.HasComment("Auth: Manages users across multiple sites."));

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<kot>(entity =>
        {
            entity.HasKey(e => e.kot_id).HasName("kot_pkey");

            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.printed).HasDefaultValue(false);

            //entity.HasOne(d => d.order).WithOne(p => p.kot)
            //    .OnDelete(DeleteBehavior.Cascade)
            //    .HasConstraintName("kot_order_id_fkey");
        });

        modelBuilder.Entity<MfaAmrClaim>(entity =>
        {
            entity.HasKey(e => e.id).HasName("amr_id_pk");

            entity.ToTable("mfa_amr_claims", "auth", tb => tb.HasComment("auth: stores authenticator method reference claims for multi factor authentication"));

            entity.Property(e => e.id).ValueGeneratedNever();

            entity.HasOne(d => d.session).WithMany(p => p.mfa_amr_claims).HasConstraintName("mfa_amr_claims_session_id_fkey");
        });

        modelBuilder.Entity<MFAChallenge>(entity =>
        {
            entity.HasKey(e => e.id).HasName("mfa_challenges_pkey");

            entity.ToTable("mfa_challenges", "auth", tb => tb.HasComment("auth: stores metadata about challenge requests made"));

            entity.Property(e => e.id).ValueGeneratedNever();

            entity.HasOne(d => d.factor).WithMany(p => p.mfa_challenges).HasConstraintName("mfa_challenges_auth_factor_id_fkey");
        });

        modelBuilder.Entity<mfa_factor>(entity =>
        {
            entity.HasKey(e => e.id).HasName("mfa_factors_pkey");

            entity.ToTable("mfa_factors", "auth", tb => tb.HasComment("auth: stores metadata about factors"));

            entity.HasIndex(e => new { e.friendly_name, e.user_id }, "mfa_factors_user_friendly_name_unique")
                .IsUnique()
                .HasFilter("(TRIM(BOTH FROM friendly_name) <> ''::text)");

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.last_webauthn_challenge_data).HasComment("Stores the latest WebAuthn challenge data including attestation/assertion for customer verification");

            entity.HasOne(d => d.user).WithMany(p => p.mfa_factors).HasConstraintName("mfa_factors_user_id_fkey");
        });

        modelBuilder.Entity<migration>(entity =>
        {
            entity.HasKey(e => e.id).HasName("migrations_pkey");

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.executed_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<OauthAuthorization>(entity =>
        {
            entity.HasKey(e => e.id).HasName("oauth_authorizations_pkey");

            entity.HasIndex(e => e.expires_at, "oauth_auth_pending_exp_idx").HasFilter("(status = 'pending'::auth.oauth_authorization_status)");

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.expires_at).HasDefaultValueSql("(now() + '00:03:00'::interval)");

            entity.HasOne(d => d.client).WithMany(p => p.oauth_authorizations).HasConstraintName("oauth_authorizations_client_id_fkey");

            entity.HasOne(d => d.user).WithMany(p => p.oauth_authorizations)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauth_authorizations_user_id_fkey");
        });

        modelBuilder.Entity<oauth_client>(entity =>
        {
            entity.HasKey(e => e.id).HasName("oauth_clients_pkey");

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<oauth_client_state>(entity =>
        {
            entity.HasKey(e => e.id).HasName("oauth_client_states_pkey");

            entity.ToTable("oauth_client_states", "auth", tb => tb.HasComment("Stores OAuth states for third-party provider authentication flows where Supabase acts as the OAuth client."));

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<OauthConsent>(entity =>
        {
            entity.HasKey(e => e.id).HasName("oauth_consents_pkey");

            entity.HasIndex(e => e.client_id, "oauth_consents_active_client_idx").HasFilter("(revoked_at IS NULL)");

            entity.HasIndex(e => new { e.user_id, e.client_id }, "oauth_consents_active_user_client_idx").HasFilter("(revoked_at IS NULL)");

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.granted_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.client).WithMany(p => p.oauth_consents).HasConstraintName("oauth_consents_client_id_fkey");

            entity.HasOne(d => d.user).WithMany(p => p.oauth_consents).HasConstraintName("oauth_consents_user_id_fkey");
        });

        modelBuilder.Entity<object2>(entity =>
        {
            entity.HasKey(e => e.id).HasName("objects_pkey");

            entity.HasIndex(e => new { e.name, e.bucket_id, e.level }, "idx_name_bucket_level_unique")
                .IsUnique()
                .UseCollation(new[] { "C", null, null });

            entity.HasIndex(e => new { e.bucket_id, e.name }, "idx_objects_bucket_id_name").UseCollation(new[] { null, "C" });

            entity.HasIndex(e => e.name, "name_prefix_search").HasOperators(new[] { "text_pattern_ops" });

            entity.HasIndex(e => new { e.bucket_id, e.level, e.name }, "objects_bucket_id_level_idx")
                .IsUnique()
                .UseCollation(new[] { null, null, "C" });

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.last_accessed_at).HasDefaultValueSql("now()");
            entity.Property(e => e.owner).HasComment("Field is deprecated, use owner_id instead");
            entity.Property(e => e.path_tokens).HasComputedColumnSql("string_to_array(name, '/'::text)", true);
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");

            //entity.HasOne(d => d.bucket).WithMany(p => p.objects2).HasConstraintName("objects_bucketId_fkey");
        });

        modelBuilder.Entity<OneTimeToken>(entity =>
        {
            entity.HasKey(e => e.id).HasName("one_time_tokens_pkey");

            entity.HasIndex(e => e.relates_to, "one_time_tokens_relates_to_hash_idx").HasMethod("hash");

            entity.HasIndex(e => e.token_hash, "one_time_tokens_token_hash_hash_idx").HasMethod("hash");

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.user).WithMany(p => p.one_time_tokens).HasConstraintName("one_time_tokens_user_id_fkey");
        });

        modelBuilder.Entity<order>(entity =>
        {
            entity.HasKey(e => e.order_id).HasName("orders_pkey");

            entity.Property(e => e.created_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.orders).HasConstraintName("orders_created_by_fkey");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.order_item_id).HasName("order_items_pkey");

            entity.HasOne(d => d.order).WithMany(p => p.order_items).HasConstraintName("order_items_order_id_fkey");

            entity.HasOne(d => d.product).WithMany(p => p.order_items)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_items_product_id_fkey");
        });

        modelBuilder.Entity<payment>(entity =>
        {
            entity.HasKey(e => e.payment_id).HasName("payments_pkey");

            entity.Property(e => e.created_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.order).WithMany(p => p.payments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("payments_order_id_fkey");
        });

        modelBuilder.Entity<prefix>(entity =>
        {
            entity.HasKey(e => new { e.bucket_id, e.level, e.name }).HasName("prefixes_pkey");

            entity.Property(e => e.level).HasComputedColumnSql("storage.get_level(name)", true);
            entity.Property(e => e.name).UseCollation("C");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");

            //entity.HasOne(d => d.bucket).WithMany(p => p.prefixes)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("prefixes_bucketId_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.product_id).HasName("products_pkey");

            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.is_active).HasDefaultValue(true);

            entity.HasOne(d => d.brand).WithMany(p => p.products).HasConstraintName("products_brand_id_fkey");

            entity.HasOne(d => d.category).WithMany(p => p.products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_category_id_fkey");

            entity.HasOne(d => d.flavor).WithMany(p => p.products).HasConstraintName("products_flavor_id_fkey");

            entity.HasOne(d => d.serving_type).WithMany(p => p.products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_serving_type_id_fkey");
        });

        modelBuilder.Entity<ProductRecipe>(entity =>
        {
            entity.HasKey(e => new { e.product_id, e.ingredient_id }).HasName("product_recipes_pkey");

            entity.HasOne(d => d.ingredient).WithMany(p => p.product_recipes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_recipes_ingredient_id_fkey");

            entity.HasOne(d => d.product).WithMany(p => p.product_recipes).HasConstraintName("product_recipes_product_id_fkey");
        });

        modelBuilder.Entity<purchase_bill>(entity =>
        {
            entity.HasKey(e => e.purchase_bill_id).HasName("purchase_bills_pkey");

            entity.Property(e => e.created_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.vendor).WithMany(p => p.purchase_bills).HasConstraintName("purchase_bills_vendor_id_fkey");
        });

        modelBuilder.Entity<purchase_bill_parsed_item>(entity =>
        {
            entity.HasKey(e => e.parsed_item_id).HasName("purchase_bill_parsed_items_pkey");

            entity.HasOne(d => d.purchase_bill).WithMany(p => p.purchase_bill_parsed_items)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("purchase_bill_parsed_items_purchase_bill_id_fkey");
        });

        modelBuilder.Entity<purchase_item>(entity =>
        {
            entity.HasKey(e => e.purchase_item_id).HasName("purchase_items_pkey");

            entity.HasOne(d => d.ingredient).WithMany(p => p.purchase_items).HasConstraintName("purchase_items_ingredient_id_fkey");

            entity.HasOne(d => d.purchase_bill).WithMany(p => p.purchase_items)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("purchase_items_purchase_bill_id_fkey");
        });

        modelBuilder.Entity<refresh_token>(entity =>
        {
            entity.HasKey(e => e.id).HasName("refresh_tokens_pkey");

            entity.ToTable("refresh_tokens", "auth", tb => tb.HasComment("Auth: Store of tokens used to refresh JWT tokens once they expire."));

            entity.HasOne(d => d.session).WithMany(p => p.refresh_tokens)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("refresh_tokens_session_id_fkey");
        });

        modelBuilder.Entity<role>(entity =>
        {
            entity.HasKey(e => e.role_id).HasName("roles_pkey");
        });

        modelBuilder.Entity<s3_multipart_upload>(entity =>
        {
            entity.HasKey(e => e.id).HasName("s3_multipart_uploads_pkey");

            entity.HasIndex(e => new { e.bucket_id, e.key, e.created_at }, "idx_multipart_uploads_list").UseCollation(new[] { null, "C", null });

            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.in_progress_size).HasDefaultValue(0L);
            entity.Property(e => e.key).UseCollation("C");

            //entity.HasOne(d => d.bucket).WithMany(p => p.s3_multipart_uploads)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("s3_multipart_uploads_bucket_id_fkey");
        });

        modelBuilder.Entity<s3_multipart_uploads_part>(entity =>
        {
            entity.HasKey(e => e.id).HasName("s3_multipart_uploads_parts_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.key).UseCollation("C");
            entity.Property(e => e.size).HasDefaultValue(0L);

            //entity.HasOne(d => d.bucket).WithMany(p => p.s3_multipart_uploads_parts)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("s3_multipart_uploads_parts_bucket_id_fkey");

            entity.HasOne(d => d.upload).WithMany(p => p.s3_multipart_uploads_parts).HasConstraintName("s3_multipart_uploads_parts_upload_id_fkey");
        });

        modelBuilder.Entity<saml_provider>(entity =>
        {
            entity.HasKey(e => e.id).HasName("saml_providers_pkey");

            entity.ToTable("saml_providers", "auth", tb => tb.HasComment("Auth: Manages SAML Identity Provider connections."));

            entity.Property(e => e.id).ValueGeneratedNever();

            entity.HasOne(d => d.sso_provider).WithMany(p => p.saml_providers).HasConstraintName("saml_providers_sso_provider_id_fkey");
        });

        modelBuilder.Entity<saml_relay_state>(entity =>
        {
            entity.HasKey(e => e.id).HasName("saml_relay_states_pkey");

            entity.ToTable("saml_relay_states", "auth", tb => tb.HasComment("Auth: Contains SAML Relay State information for each Service Provider initiated login."));

            entity.Property(e => e.id).ValueGeneratedNever();

            entity.HasOne(d => d.flow_state).WithMany(p => p.saml_relay_states)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("saml_relay_states_flow_state_id_fkey");

            entity.HasOne(d => d.sso_provider).WithMany(p => p.saml_relay_states).HasConstraintName("saml_relay_states_sso_provider_id_fkey");
        });

        modelBuilder.Entity<schema_migration>(entity =>
        {
            entity.HasKey(e => e.version).HasName("schema_migrations_pkey");

            entity.ToTable("schema_migrations", "auth", tb => tb.HasComment("Auth: Manages updates to the auth system."));
        });

        modelBuilder.Entity<schema_migration1>(entity =>
        {
            entity.HasKey(e => e.version).HasName("schema_migrations_pkey");

            entity.Property(e => e.version).ValueGeneratedNever();
        });

        modelBuilder.Entity<serving_type>(entity =>
        {
            entity.HasKey(e => e.serving_type_id).HasName("serving_types_pkey");
        });

        modelBuilder.Entity<session>(entity =>
        {
            entity.HasKey(e => e.id).HasName("sessions_pkey");

            entity.ToTable("sessions", "auth", tb => tb.HasComment("Auth: Stores session data associated to a user."));

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.not_after).HasComment("Auth: Not after is a nullable column that contains a timestamp after which the session should be regarded as expired.");
            entity.Property(e => e.refresh_token_counter).HasComment("Holds the ID (counter) of the last issued refresh token.");
            entity.Property(e => e.refresh_token_hmac_key).HasComment("Holds a HMAC-SHA256 key used to sign refresh tokens for this session.");

            entity.HasOne(d => d.oauth_client).WithMany(p => p.sessions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sessions_oauth_client_id_fkey");

            entity.HasOne(d => d.user).WithMany(p => p.sessions).HasConstraintName("sessions_user_id_fkey");
        });

        modelBuilder.Entity<sso_domain>(entity =>
        {
            entity.HasKey(e => e.id).HasName("sso_domains_pkey");

            entity.ToTable("sso_domains", "auth", tb => tb.HasComment("Auth: Manages SSO email address domain mapping to an SSO Identity Provider."));

            entity.Property(e => e.id).ValueGeneratedNever();

            entity.HasOne(d => d.sso_provider).WithMany(p => p.sso_domains).HasConstraintName("sso_domains_sso_provider_id_fkey");
        });

        modelBuilder.Entity<sso_provider>(entity =>
        {
            entity.HasKey(e => e.id).HasName("sso_providers_pkey");

            entity.ToTable("sso_providers", "auth", tb => tb.HasComment("Auth: Manages SSO identity provider information; see saml_providers for SAML."));

            entity.HasIndex(e => e.resource_id, "sso_providers_resource_id_pattern_idx").HasOperators(new[] { "text_pattern_ops" });

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.resource_id).HasComment("Auth: Uniquely identifies a SSO provider according to a user-chosen resource ID (case insensitive), useful in infrastructure as code.");
        });

        modelBuilder.Entity<stock>(entity =>
        {
            entity.HasKey(e => e.ingredient_id).HasName("stock_pkey");

            entity.Property(e => e.ingredient_id).ValueGeneratedNever();

            entity.HasOne(d => d.ingredient).WithOne(p => p.stock)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stock_ingredient_id_fkey");
        });

        modelBuilder.Entity<stock_adjustment>(entity =>
        {
            entity.HasKey(e => e.adjustment_id).HasName("stock_adjustments_pkey");

            entity.Property(e => e.created_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.ingredient).WithMany(p => p.stock_adjustments).HasConstraintName("stock_adjustments_ingredient_id_fkey");
        });

        modelBuilder.Entity<stock_transaction>(entity =>
        {
            entity.HasKey(e => e.stock_txn_id).HasName("stock_transactions_pkey");

            entity.Property(e => e.created_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.ingredient).WithMany(p => p.stock_transactions).HasConstraintName("stock_transactions_ingredient_id_fkey");
        });

        modelBuilder.Entity<subscription>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_subscription");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.created_at).HasDefaultValueSql("timezone('utc'::text, now())");
        });

        modelBuilder.Entity<unit_conversion>(entity =>
        {
            entity.HasKey(e => new { e.from_unit, e.to_unit }).HasName("unit_conversions_pkey");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("users_pkey");

            entity.ToTable("users", "auth", tb => tb.HasComment("Auth: Stores user login data within a secure schema."));

            entity.HasIndex(e => e.confirmation_token, "confirmation_token_idx")
                .IsUnique()
                .HasFilter("((confirmation_token)::text !~ '^[0-9 ]*$'::text)");

            entity.HasIndex(e => e.email_change_token_current, "email_change_token_current_idx")
                .IsUnique()
                .HasFilter("((email_change_token_current)::text !~ '^[0-9 ]*$'::text)");

            entity.HasIndex(e => e.email_change_token_new, "email_change_token_new_idx")
                .IsUnique()
                .HasFilter("((email_change_token_new)::text !~ '^[0-9 ]*$'::text)");

            entity.HasIndex(e => e.reauthentication_token, "reauthentication_token_idx")
                .IsUnique()
                .HasFilter("((reauthentication_token)::text !~ '^[0-9 ]*$'::text)");

            entity.HasIndex(e => e.recovery_token, "recovery_token_idx")
                .IsUnique()
                .HasFilter("((recovery_token)::text !~ '^[0-9 ]*$'::text)");

            entity.HasIndex(e => e.email, "users_email_partial_key")
                .IsUnique()
                .HasFilter("(is_sso_user = false)");

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.confirmed_at).HasComputedColumnSql("LEAST(email_confirmed_at, phone_confirmed_at)", true);
            entity.Property(e => e.email_change_confirm_status).HasDefaultValue((short)0);
            entity.Property(e => e.email_change_token_current).HasDefaultValueSql("''::character varying");
            entity.Property(e => e.is_anonymous).HasDefaultValue(false);
            entity.Property(e => e.is_sso_user)
                .HasDefaultValue(false)
                .HasComment("Auth: Set this column to true when the account comes from SSO. These accounts can have duplicate emails.");
            entity.Property(e => e.phone).HasDefaultValueSql("NULL::character varying");
            entity.Property(e => e.phone_change).HasDefaultValueSql("''::character varying");
            entity.Property(e => e.phone_change_token).HasDefaultValueSql("''::character varying");
            entity.Property(e => e.reauthentication_token).HasDefaultValueSql("''::character varying");
        });

        modelBuilder.Entity<user1>(entity =>
        {
            entity.HasKey(e => e.user_id).HasName("users_pkey");

            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.is_active).HasDefaultValue(true);

            entity.HasOne(d => d.role).WithMany(p => p.user1s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");
        });

        modelBuilder.Entity<vector_index>(entity =>
        {
            entity.HasKey(e => e.id).HasName("vector_indexes_pkey");

            entity.HasIndex(e => new { e.name, e.bucket_id }, "vector_indexes_name_bucket_id_idx")
                .IsUnique()
                .UseCollation(new[] { "C", null });

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.name).UseCollation("C");
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.bucket).WithMany(p => p.vector_indices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vector_indexes_bucket_id_fkey");
        });

        modelBuilder.Entity<vendor>(entity =>
        {
            entity.HasKey(e => e.vendor_id).HasName("vendors_pkey");

            entity.Property(e => e.is_active).HasDefaultValue(true);
        });
        modelBuilder.HasSequence<int>("seq_schema_version", "graphql").IsCyclic();

        OnModelCreatingPartial(modelBuilder);
    }
    */
	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
