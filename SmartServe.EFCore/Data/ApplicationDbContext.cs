using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Entities;

namespace SmartServe.EFCore.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Flavor> Flavors { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientAlias> IngredientAliases { get; set; }

    public virtual DbSet<Kot> Kots { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductRecipe> ProductRecipes { get; set; }

    public virtual DbSet<PurchaseBill> PurchaseBills { get; set; }

    public virtual DbSet<PurchaseBillParsedItem> PurchaseBillParsedItems { get; set; }

    public virtual DbSet<PurchaseItem> PurchaseItems { get; set; }

    public virtual DbSet<RestaurantTable> RestaurantTables { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ServingType> ServingTypes { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<StockAdjustment> StockAdjustments { get; set; }

    public virtual DbSet<StockTransaction> StockTransactions { get; set; }

    public virtual DbSet<TableStatus> TableStatuses { get; set; }

    public virtual DbSet<UnitConversion> UnitConversions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

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

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("brands_pkey");

            entity.ToTable("brands");

            entity.HasIndex(e => e.Name, "brands_name_key").IsUnique();

            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.HasIndex(e => e.Name, "categories_name_key").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Flavor>(entity =>
        {
            entity.HasKey(e => e.FlavorId).HasName("flavors_pkey");

            entity.ToTable("flavors");

            entity.HasIndex(e => e.Name, "flavors_name_key").IsUnique();

            entity.Property(e => e.FlavorId).HasColumnName("flavor_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("ingredients_pkey");

            entity.ToTable("ingredients");

            entity.HasIndex(e => e.Name, "ingredients_name_key").IsUnique();

            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.BaseUnit)
                .HasMaxLength(20)
                .HasColumnName("base_unit");
            entity.Property(e => e.IdealVariancePercent)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("8.0")
                .HasColumnName("ideal_variance_percent");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
        });

        modelBuilder.Entity<IngredientAlias>(entity =>
        {
            entity.HasKey(e => e.AliasId).HasName("ingredient_aliases_pkey");

            entity.ToTable("ingredient_aliases");

            entity.Property(e => e.AliasId).HasColumnName("alias_id");
            entity.Property(e => e.AliasName)
                .HasMaxLength(150)
                .HasColumnName("alias_name");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.IngredientAliases)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ingredient_aliases_ingredient_id_fkey");
        });

        modelBuilder.Entity<Kot>(entity =>
        {
            entity.HasKey(e => e.KotId).HasName("kot_pkey");

            entity.ToTable("kot");

            entity.HasIndex(e => e.OrderId, "kot_order_id_key").IsUnique();

            entity.Property(e => e.KotId).HasColumnName("kot_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Printed)
                .HasDefaultValue(false)
                .HasColumnName("printed");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");

            entity.HasOne(d => d.Order).WithOne(p => p.Kot)
                .HasForeignKey<Kot>(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("kot_order_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.HasIndex(e => e.CreatedAt, "idx_orders_created_at");

            entity.HasIndex(e => e.OrderNumber, "orders_order_number_key").IsUnique();

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ClosedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("closed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(20)
                .HasColumnName("order_number");
            entity.Property(e => e.OrderType)
                .HasMaxLength(20)
                .HasColumnName("order_type");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("orders_created_by_fkey");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("orders_status_id_fkey");

            entity.HasOne(d => d.Table).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TableId)
                .HasConstraintName("orders_table_id_fkey");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("order_items_pkey");

            entity.ToTable("order_items");

            entity.HasIndex(e => e.OrderId, "idx_order_items_order_id");

            entity.HasIndex(e => e.ProductId, "idx_order_items_product_id");

            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PriceSnapshot)
                .HasPrecision(10, 2)
                .HasColumnName("price_snapshot");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("order_items_order_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_items_product_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
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

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("products_pkey");

            entity.ToTable("products");

            entity.HasIndex(e => e.CategoryId, "idx_products_category");

            entity.HasIndex(e => e.FlavorId, "idx_products_flavor");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.FlavorId).HasColumnName("flavor_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ServingTypeId).HasColumnName("serving_type_id");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("products_brand_id_fkey");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_category_id_fkey");

            entity.HasOne(d => d.Flavor).WithMany(p => p.Products)
                .HasForeignKey(d => d.FlavorId)
                .HasConstraintName("products_flavor_id_fkey");

            entity.HasOne(d => d.ServingType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ServingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_serving_type_id_fkey");
        });

        modelBuilder.Entity<ProductRecipe>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.IngredientId }).HasName("product_recipes_pkey");

            entity.ToTable("product_recipes");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.QtyRequired)
                .HasPrecision(10, 2)
                .HasColumnName("qty_required");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.ProductRecipes)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_recipes_ingredient_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductRecipes)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_recipes_product_id_fkey");
        });

        modelBuilder.Entity<PurchaseBill>(entity =>
        {
            entity.HasKey(e => e.PurchaseBillId).HasName("purchase_bills_pkey");

            entity.ToTable("purchase_bills");

            entity.HasIndex(e => e.VendorId, "idx_purchase_bill_vendor");

            entity.Property(e => e.PurchaseBillId).HasColumnName("purchase_bill_id");
            entity.Property(e => e.BillDate).HasColumnName("bill_date");
            entity.Property(e => e.BillNumber)
                .HasMaxLength(50)
                .HasColumnName("bill_number");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.OcrStatus)
                .HasMaxLength(20)
                .HasColumnName("ocr_status");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(10, 2)
                .HasColumnName("total_amount");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseBills)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("purchase_bills_vendor_id_fkey");
        });

        modelBuilder.Entity<PurchaseBillParsedItem>(entity =>
        {
            entity.HasKey(e => e.ParsedItemId).HasName("purchase_bill_parsed_items_pkey");

            entity.ToTable("purchase_bill_parsed_items");

            entity.Property(e => e.ParsedItemId).HasColumnName("parsed_item_id");
            entity.Property(e => e.ConfidenceScore)
                .HasPrecision(5, 2)
                .HasColumnName("confidence_score");
            entity.Property(e => e.ExtractedName)
                .HasMaxLength(200)
                .HasColumnName("extracted_name");
            entity.Property(e => e.PurchaseBillId).HasColumnName("purchase_bill_id");
            entity.Property(e => e.Quantity)
                .HasPrecision(10, 2)
                .HasColumnName("quantity");
            entity.Property(e => e.RawItemText).HasColumnName("raw_item_text");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .HasColumnName("unit");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");

            entity.HasOne(d => d.PurchaseBill).WithMany(p => p.PurchaseBillParsedItems)
                .HasForeignKey(d => d.PurchaseBillId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("purchase_bill_parsed_items_purchase_bill_id_fkey");
        });

        modelBuilder.Entity<PurchaseItem>(entity =>
        {
            entity.HasKey(e => e.PurchaseItemId).HasName("purchase_items_pkey");

            entity.ToTable("purchase_items");

            entity.Property(e => e.PurchaseItemId).HasColumnName("purchase_item_id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.PurchaseBillId).HasColumnName("purchase_bill_id");
            entity.Property(e => e.Quantity)
                .HasPrecision(10, 2)
                .HasColumnName("quantity");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(10, 2)
                .HasColumnName("total_amount");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.PurchaseItems)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("purchase_items_ingredient_id_fkey");

            entity.HasOne(d => d.PurchaseBill).WithMany(p => p.PurchaseItems)
                .HasForeignKey(d => d.PurchaseBillId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("purchase_items_purchase_bill_id_fkey");
        });

        modelBuilder.Entity<RestaurantTable>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("restaurant_tables_pkey");

            entity.ToTable("restaurant_tables");

            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .HasColumnName("display_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "roles_role_name_key").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<ServingType>(entity =>
        {
            entity.HasKey(e => e.ServingTypeId).HasName("serving_types_pkey");

            entity.ToTable("serving_types");

            entity.HasIndex(e => e.Name, "serving_types_name_key").IsUnique();

            entity.Property(e => e.ServingTypeId).HasColumnName("serving_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("stock_pkey");

            entity.ToTable("stock");

            entity.Property(e => e.IngredientId)
                .ValueGeneratedNever()
                .HasColumnName("ingredient_id");
            entity.Property(e => e.CurrentQty)
                .HasPrecision(12, 2)
                .HasColumnName("current_qty");

            entity.HasOne(d => d.Ingredient).WithOne(p => p.Stock)
                .HasForeignKey<Stock>(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stock_ingredient_id_fkey");
        });

        modelBuilder.Entity<StockAdjustment>(entity =>
        {
            entity.HasKey(e => e.AdjustmentId).HasName("stock_adjustments_pkey");

            entity.ToTable("stock_adjustments");

            entity.Property(e => e.AdjustmentId).HasColumnName("adjustment_id");
            entity.Property(e => e.ActualQty)
                .HasPrecision(10, 2)
                .HasColumnName("actual_qty");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpectedQty)
                .HasPrecision(10, 2)
                .HasColumnName("expected_qty");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .HasColumnName("reason");
            entity.Property(e => e.VarianceQty)
                .HasPrecision(10, 2)
                .HasColumnName("variance_qty");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.StockAdjustments)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("stock_adjustments_ingredient_id_fkey");
        });

        modelBuilder.Entity<StockTransaction>(entity =>
        {
            entity.HasKey(e => e.StockTxnId).HasName("stock_transactions_pkey");

            entity.ToTable("stock_transactions");

            entity.HasIndex(e => e.IngredientId, "idx_stock_txn_ingredient");

            entity.Property(e => e.StockTxnId).HasColumnName("stock_txn_id");
            entity.Property(e => e.ChangeQty)
                .HasPrecision(10, 2)
                .HasColumnName("change_qty");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Reason)
                .HasMaxLength(30)
                .HasColumnName("reason");
            entity.Property(e => e.ReferenceId).HasColumnName("reference_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.StockTransactions)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("stock_transactions_ingredient_id_fkey");
        });

        modelBuilder.Entity<TableStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("table_status_pkey");

            entity.ToTable("table_status");

            entity.HasIndex(e => e.StatusCode, "table_status_status_code_key").IsUnique();

            entity.Property(e => e.StatusId).HasColumnName("status_id");
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

        modelBuilder.Entity<UnitConversion>(entity =>
        {
            entity.HasKey(e => new { e.FromUnit, e.ToUnit }).HasName("unit_conversions_pkey");

            entity.ToTable("unit_conversions");

            entity.Property(e => e.FromUnit)
                .HasMaxLength(20)
                .HasColumnName("from_unit");
            entity.Property(e => e.ToUnit)
                .HasMaxLength(20)
                .HasColumnName("to_unit");
            entity.Property(e => e.ConversionFactor)
                .HasPrecision(10, 4)
                .HasColumnName("conversion_factor");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("vendors_pkey");

            entity.ToTable("vendors");

            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
            entity.Property(e => e.GstNo)
                .HasMaxLength(20)
                .HasColumnName("gst_no");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
