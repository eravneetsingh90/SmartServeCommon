using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Models;

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

	public virtual DbSet<BrandEntity> Brands { get; set; }

	public virtual DbSet<CategoryEntity> Categories { get; set; }

	public virtual DbSet<OrderEntity> Orders { get; set; }

	public virtual DbSet<OrderItemEntity> OrderItems { get; set; }

	public virtual DbSet<PaymentEntity> Payments { get; set; }

	public virtual DbSet<ProductEntity> Products { get; set; }

	public virtual DbSet<ProductIngredientEntity> ProductIngredients { get; set; }

	public virtual DbSet<ProductVariantEntity> ProductVariants { get; set; }

	public virtual DbSet<RestaurantTableEntity> RestaurantTables { get; set; }

	public virtual DbSet<RoleEntity> Roles { get; set; }

	public virtual DbSet<StockEntity> Stocks { get; set; }

	public virtual DbSet<StockTransactionEntity> StockTransactions { get; set; }

	public virtual DbSet<TableStatusEntity> TableStatuses { get; set; }

	public virtual DbSet<UserEntity> Users { get; set; }

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

		modelBuilder.Entity<BrandEntity>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("brands_pkey");

			entity.ToTable("brands");

			entity.HasIndex(e => e.Name, "brands_name_key").IsUnique();

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.IsActive)
				.HasDefaultValue(true)
				.HasColumnName("is_active");
			entity.Property(e => e.Name)
				.HasMaxLength(100)
				.HasColumnName("name");
		});

		modelBuilder.Entity<CategoryEntity>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("categories_pkey");

			entity.ToTable("categories");

			entity.HasIndex(e => e.Name, "categories_name_key").IsUnique();

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.DisplayOrder)
				.HasDefaultValue(0)
				.HasColumnName("display_order");
			entity.Property(e => e.IsActive)
				.HasDefaultValue(true)
				.HasColumnName("is_active");
			entity.Property(e => e.Name)
				.HasMaxLength(100)
				.HasColumnName("name");
		});

		modelBuilder.Entity<OrderEntity>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("orders_pkey");

			entity.ToTable("orders");

			entity.HasIndex(e => e.CreatedAt, "idx_orders_created");

			entity.HasIndex(e => e.OrderNumber, "orders_order_number_key").IsUnique();

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.ClosedAt).HasColumnName("closed_at");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("now()")
				.HasColumnName("created_at");
			entity.Property(e => e.DiscountReason)
				.HasMaxLength(100)
				.HasColumnName("discount_reason");
			entity.Property(e => e.DiscountType)
				.HasMaxLength(10)
				.HasColumnName("discount_type");
			entity.Property(e => e.DiscountValue)
				.HasPrecision(10, 2)
				.HasDefaultValueSql("0")
				.HasColumnName("discount_value");
			entity.Property(e => e.OrderNumber)
				.HasMaxLength(20)
				.HasColumnName("order_number");
			entity.Property(e => e.OrderType)
				.HasMaxLength(20)
				.HasColumnName("order_type");
			entity.Property(e => e.IsTracked)
				.HasDefaultValue(false)
				.HasColumnName("is_tracked");
			entity.Property(e => e.StatusId).HasColumnName("status_id");
			entity.Property(e => e.TableId).HasColumnName("table_id");
			entity.Property(e => e.OriginalAmount)
				.HasPrecision(10, 2)
				.HasDefaultValueSql("0")
				.HasColumnName("original_amount");
			entity.Property(e => e.TotalAmount)
				.HasPrecision(10, 2)
				.HasDefaultValueSql("0")
				.HasColumnName("total_amount");

			entity.HasOne(d => d.Status).WithMany(p => p.Orders)
				.HasForeignKey(d => d.StatusId)
				.HasConstraintName("orders_status_id_fkey");

			entity.HasOne(d => d.Table).WithMany(p => p.Orders)
				.HasForeignKey(d => d.TableId)
				.HasConstraintName("orders_table_id_fkey");
		});

		modelBuilder.Entity<OrderItemEntity>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("order_items_pkey");

			entity.ToTable("order_items");

			entity.HasIndex(e => e.OrderId, "idx_order_items_order");

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.DiscountAmount)
				.HasPrecision(10, 2)
				.HasDefaultValueSql("0")
				.HasColumnName("discount_amount");
			entity.Property(e => e.OrderId).HasColumnName("order_id");
			entity.Property(e => e.PriceSnapshot)
				.HasPrecision(10, 2)
				.HasColumnName("price_snapshot");
			entity.Property(e => e.Quantity).HasColumnName("quantity");
			entity.Property(e => e.VariantId).HasColumnName("variant_id");

			entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
				.HasForeignKey(d => d.OrderId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("order_items_order_id_fkey");

			entity.HasOne(d => d.Variant).WithMany(p => p.OrderItems)
				.HasForeignKey(d => d.VariantId)
				.HasConstraintName("order_items_variant_id_fkey");
		});

		modelBuilder.Entity<PaymentEntity>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("payments_pkey");

			entity.ToTable("payments");

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Amount)
				.HasPrecision(10, 2)
				.HasColumnName("amount");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("now()")
				.HasColumnName("created_at");
			entity.Property(e => e.Mode)
				.HasMaxLength(20)
				.HasColumnName("mode");
			entity.Property(e => e.OrderId).HasColumnName("order_id");
			entity.Property(e => e.Status)
				.HasMaxLength(20)
				.HasColumnName("status");

			entity.HasOne(d => d.Order).WithMany(p => p.Payments)
				.HasForeignKey(d => d.OrderId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("payments_order_id_fkey");
		});

		modelBuilder.Entity<ProductEntity>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("products_pkey");

			entity.ToTable("products");

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.CategoryId).HasColumnName("category_id");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("now()")
				.HasColumnName("created_at");
			entity.Property(e => e.DisplayOrder)
				.HasDefaultValue(0)
				.HasColumnName("display_order");
			entity.Property(e => e.FoodType)
				.HasMaxLength(10)
				.HasColumnName("food_type");
			entity.Property(e => e.IsActive)
				.HasDefaultValue(true)
				.HasColumnName("is_active");
			entity.Property(e => e.Name)
				.HasMaxLength(150)
				.HasColumnName("name");

			entity.HasOne(d => d.Category).WithMany(p => p.Products)
				.HasForeignKey(d => d.CategoryId)
				.HasConstraintName("products_category_id_fkey");
		});

		modelBuilder.Entity<ProductIngredientEntity>(entity =>
		{
			entity.HasKey(e => new { e.ProductVariantId, e.IngredientVariantId }).HasName("product_ingredients_pkey");

			entity.ToTable("product_ingredients");

			entity.HasIndex(e => e.ProductVariantId, "idx_pi_product_variant");

			entity.Property(e => e.ProductVariantId).HasColumnName("product_variant_id");
			entity.Property(e => e.IngredientVariantId).HasColumnName("ingredient_variant_id");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("now()")
				.HasColumnName("created_at");
			entity.Property(e => e.Quantity)
				.HasPrecision(10, 3)
				.HasColumnName("quantity");
			entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

			entity.HasOne(d => d.IngredientVariant).WithMany(p => p.ProductIngredientIngredientVariants)
				.HasForeignKey(d => d.IngredientVariantId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("product_ingredients_ingredient_variant_id_fkey");

			entity.HasOne(d => d.ProductVariant).WithMany(p => p.ProductIngredientProductVariants)
				.HasForeignKey(d => d.ProductVariantId)
				.HasConstraintName("product_ingredients_product_variant_id_fkey");
		});

		modelBuilder.Entity<ProductVariantEntity>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("product_variants_pkey");

			entity.ToTable("product_variants");

			entity.HasIndex(e => e.ProductId, "idx_variants_product");

			entity.HasIndex(e => new { e.ProductId, e.BrandId, e.VariantName }, "product_variants_product_id_brand_id_variant_name_key").IsUnique();

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.BrandId).HasColumnName("brand_id");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("now()")
				.HasColumnName("created_at");
			entity.Property(e => e.DisplayOrder)
				.HasDefaultValue(0)
				.HasColumnName("display_order");
			entity.Property(e => e.IsActive)
				.HasDefaultValue(true)
				.HasColumnName("is_active");
			entity.Property(e => e.Price)
				.HasPrecision(10, 2)
				.HasColumnName("price");
			entity.Property(e => e.ProductId).HasColumnName("product_id");
			entity.Property(e => e.VariantName)
				.HasMaxLength(100)
				.HasColumnName("variant_name");

			entity.HasOne(d => d.Brand).WithMany(p => p.ProductVariants)
				.HasForeignKey(d => d.BrandId)
				.HasConstraintName("product_variants_brand_id_fkey");

			entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
				.HasForeignKey(d => d.ProductId)
				.HasConstraintName("product_variants_product_id_fkey");
		});

		modelBuilder.Entity<RestaurantTableEntity>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("restaurant_tables_pkey");

			entity.ToTable("restaurant_tables");

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("now()")
				.HasColumnName("created_at");
			entity.Property(e => e.DisplayName)
				.HasMaxLength(50)
				.HasColumnName("display_name");
			entity.Property(e => e.IsActive)
				.HasDefaultValue(true)
				.HasColumnName("is_active");
		});

		modelBuilder.Entity<RoleEntity>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("roles_pkey");

			entity.ToTable("roles");

			entity.HasIndex(e => e.RoleName, "roles_role_name_key").IsUnique();

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.RoleName)
				.HasMaxLength(50)
				.HasColumnName("role_name");
		});

		modelBuilder.Entity<StockEntity>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("stock_pkey");

			entity.ToTable("stock");

			entity.HasIndex(e => e.VariantId, "idx_stock_variant");

			entity.HasIndex(e => new { e.ItemType, e.VariantId }, "stock_item_type_variant_id_key").IsUnique();

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("now()")
				.HasColumnName("created_at");
			entity.Property(e => e.IsActive)
				.HasDefaultValue(true)
				.HasColumnName("is_active");
			entity.Property(e => e.ItemType)
				.HasMaxLength(20)
				.HasColumnName("item_type");
			entity.Property(e => e.MinStockLevel)
				.HasPrecision(10, 2)
				.HasDefaultValueSql("0")
				.HasColumnName("min_stock_level");
			entity.Property(e => e.Unit)
				.HasMaxLength(20)
				.HasColumnName("unit");
			entity.Property(e => e.VariantId).HasColumnName("variant_id");

			entity.HasOne(d => d.Variant).WithMany(p => p.Stocks)
				.HasForeignKey(d => d.VariantId)
				.HasConstraintName("stock_variant_id_fkey");
		});

		modelBuilder.Entity<StockTransactionEntity>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("stock_transactions_pkey");

			entity.ToTable("stock_transactions");

			entity.HasIndex(e => e.CreatedAt, "idx_stock_tx_created");

			entity.HasIndex(e => e.StockId, "idx_stock_tx_stock");

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("now()")
				.HasColumnName("created_at");
			entity.Property(e => e.Quantity)
				.HasPrecision(10, 2)
				.HasColumnName("quantity");
			entity.Property(e => e.Reason)
				.HasMaxLength(30)
				.HasColumnName("reason");
			entity.Property(e => e.ReferenceId).HasColumnName("reference_id");
			entity.Property(e => e.ReferenceType)
				.HasMaxLength(20)
				.HasColumnName("reference_type");
			entity.Property(e => e.StockId).HasColumnName("stock_id");
			entity.Property(e => e.TransactionType)
				.HasMaxLength(10)
				.HasColumnName("transaction_type");

			entity.HasOne(d => d.Stock).WithMany(p => p.StockTransactions)
				.HasForeignKey(d => d.StockId)
				.HasConstraintName("stock_transactions_stock_id_fkey");
		});

		modelBuilder.Entity<TableStatusEntity>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("table_status_pkey");

			entity.ToTable("table_status");

			entity.HasIndex(e => e.StatusCode, "table_status_status_code_key").IsUnique();

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.ColorHex)
				.HasMaxLength(10)
				.HasColumnName("color_hex");
			entity.Property(e => e.StatusCode)
				.HasMaxLength(30)
				.HasColumnName("status_code");
			entity.Property(e => e.StatusName)
				.HasMaxLength(50)
				.HasColumnName("status_name");
		});

		modelBuilder.Entity<UserEntity>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("users_pkey");

			entity.ToTable("users");

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("now()")
				.HasColumnName("created_at");
			entity.Property(e => e.IsActive)
				.HasDefaultValue(true)
				.HasColumnName("is_active");
			entity.Property(e => e.Name)
				.HasMaxLength(100)
				.HasColumnName("name");
			entity.Property(e => e.PinHash)
				.HasMaxLength(255)
				.HasColumnName("pin_hash");
			entity.Property(e => e.RoleId).HasColumnName("role_id");

			entity.HasOne(d => d.Role).WithMany(p => p.Users)
				.HasForeignKey(d => d.RoleId)
				.HasConstraintName("users_role_id_fkey");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
