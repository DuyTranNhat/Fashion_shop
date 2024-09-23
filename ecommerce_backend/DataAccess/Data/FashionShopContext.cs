using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Data;

public partial class FashionShopContext : DbContext
{
    public FashionShopContext()
    {
    }

    public FashionShopContext(DbContextOptions<FashionShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Models.Attribute> Attributes { get; set; }

    public virtual DbSet<CampaignVariant> CampaignVariants { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<MarketingCampaign> MarketingCampaigns { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

    public virtual DbSet<ProductReview> ProductReviews { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }

    public virtual DbSet<Slide> Slides { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Value> Values { get; set; }
    public virtual DbSet<VariantValue> VariantValues { get; set; }

    public virtual DbSet<Variant> Variants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI\\SQLEXPRESS,1433;Initial Catalog=FashionShop;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Attribute>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PK__attribut__9090C9BB8D525DEA");

            entity.ToTable("attributes");

            entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CampaignVariant>(entity =>
        {
            entity.HasKey(e => e.CampaignVariantId).HasName("PK__campaign__16BD1BA57AFB7A29");

            entity.ToTable("campaign_variant");

            entity.Property(e => e.CampaignVariantId).HasColumnName("campaign_variant_id");
            entity.Property(e => e.CampaignId).HasColumnName("campaign_id");
            entity.Property(e => e.DiscountPercentage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("discount_percentage");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Campaign).WithMany(p => p.CampaignVariants)
                .HasForeignKey(d => d.CampaignId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__campaign___campa__66603565");

            entity.HasOne(d => d.Product).WithMany(p => p.CampaignVariants)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__campaign___produ__6754599E");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__categori__D54EE9B4AD99D938");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK__categorie__paren__5DCAEF64");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB85270A5349");

            entity.ToTable("customers");

            entity.HasIndex(e => e.Email, "UQ__customer__AB6E6164FD0124A7").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__images__DC9AC955C4C674E7");

            entity.ToTable("images");

            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.VariantId).HasColumnName("variant_id");

            entity.HasOne(d => d.Variant).WithMany(p => p.Images)
                .HasForeignKey(d => d.VariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__images__variant___5FB337D6");
        });

        modelBuilder.Entity<ProductAttribute>()
            .HasKey(pa => new { pa.ProductId, pa.AttributeId });

        // Thiết lập mối quan hệ giữa Product và ProductAttribute
        modelBuilder.Entity<ProductAttribute>()
            .HasOne(pa => pa.Product)
            .WithMany(p => p.ProductAttributes)
            .HasForeignKey(pa => pa.ProductId);

        // Thiết lập mối quan hệ giữa Attribute và ProductAttribute
        modelBuilder.Entity<ProductAttribute>()
            .HasOne(pa => pa.Attribute)
            .WithMany(a => a.ProductAttributes)
            .HasForeignKey(pa => pa.AttributeId);


        modelBuilder.Entity<VariantValue>()
                .HasKey(vv => new {
                    vv.VariantId,
                    vv.ValueId
                });

        // Thiết lập mối quan hệ giữa Variant và VariantValue
        modelBuilder.Entity<VariantValue>()
            .HasOne(vv => vv.Variant)
            .WithMany(v => v.VariantValues)
            .HasForeignKey(vv => vv.VariantId);

        // Thiết lập mối quan hệ giữa Value và VariantValue
        modelBuilder.Entity<VariantValue>()
                    .HasOne(vv => vv.Value)
                    .WithMany(v => v.VariantValues)
                    .HasForeignKey(vv => vv.ValueId);


        modelBuilder.Entity<MarketingCampaign>(entity =>
        {
            entity.HasKey(e => e.CampaignId).HasName("PK__marketin__905B681CDF38778D");

            entity.ToTable("marketing_campaigns");

            entity.Property(e => e.CampaignId).HasColumnName("campaign_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__465962290B119168");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.ShippingService)
                .HasMaxLength(50)
                .HasColumnName("shipping_service");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__customer__6383C8BA");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__order_de__3C5A40801675B765");

            entity.ToTable("order_details");

            entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.VariantId).HasColumnName("variant_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_det__order__6477ECF3");

            entity.HasOne(d => d.Variant).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.VariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_det__varia__656C112C");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__47027DF57E2E2B4A");

            entity.ToTable("products");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__products__catego__5BE2A6F2");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__products__suppli__5CD6CB2B");

            entity.HasMany(d => d.Attributes).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductAttribute",
                    r => r.HasOne<Models.Attribute>().WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__product_a__attri__619B8048"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__product_a__produ__60A75C0F"),
                    j =>
                    {
                        j.HasKey("ProductId", "AttributeId").HasName("PK__product___EE0B716E3315FA3E");
                        j.ToTable("product_attributes");
                        j.IndexerProperty<int>("ProductId").HasColumnName("product_id");
                        j.IndexerProperty<int>("AttributeId").HasColumnName("attribute_id");
                    });
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__product___60883D90516335E6");

            entity.ToTable("product_reviews");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReviewDate)
                .HasColumnType("datetime")
                .HasColumnName("review_date");

            entity.HasOne(d => d.Customer).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product_r__custo__693CA210");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product_r__produ__68487DD7");
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("PK__receipts__91F52C1FDA4880B0");

            entity.ToTable("receipts");

            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");
            entity.Property(e => e.ReceiptDate)
                .HasColumnType("datetime")
                .HasColumnName("receipt_date");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_price");
        });

        modelBuilder.Entity<ReceiptDetail>(entity =>
        {
            entity.HasKey(e => new { e.ReceiptId, e.VariantId }).HasName("PK__receipt___FF59EA94B2B54CAB");

            entity.ToTable("receipt_details");

            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");
            entity.Property(e => e.VariantId).HasColumnName("variant_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Receipt).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.ReceiptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__receipt_d__recei__6A30C649");

            entity.HasOne(d => d.Variant).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.VariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__receipt_d__varia__6B24EA82");
        });

        modelBuilder.Entity<Slide>(entity =>
        {
            entity.HasKey(e => e.SlideId).HasName("PK__slide__39AF7A8EDE045210");

            entity.ToTable("slide");

            entity.Property(e => e.SlideId).HasColumnName("slide_id");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("link");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__supplier__6EE594E87481225C");

            entity.ToTable("suppliers");

            entity.HasIndex(e => e.Email, "UQ__supplier__AB6E6164A4C709AE").IsUnique();

            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Value>(entity =>
        {
            entity.HasKey(e => e.ValueId).HasName("PK__values__0FECE2829EE927C7");

            entity.ToTable("values");

            entity.Property(e => e.ValueId).HasColumnName("value_id");
            entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
            entity.Property(e => e.Value1)
                .HasMaxLength(255)
                .HasColumnName("value");

            entity.HasOne(d => d.Attribute).WithMany(p => p.Values)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__values__attribut__628FA481");
        });

        modelBuilder.Entity<Variant>(entity =>
        {
            entity.HasKey(e => e.VariantId).HasName("PK__variants__EACC68B7C4ED4FD8");

            entity.ToTable("variants");

            entity.Property(e => e.VariantId).HasColumnName("variant_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.VariantName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("variant_name");

            entity.HasOne(d => d.Product).WithMany(p => p.Variants)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__variants__produc__5EBF139D");

            entity.HasMany(d => d.Values).WithMany(p => p.Variants)
                .UsingEntity<Dictionary<string, object>>(
                    "VariantValue",
                    r => r.HasOne<Value>().WithMany()
                        .HasForeignKey("ValueId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__variant_v__value__6D0D32F4"),
                    l => l.HasOne<Variant>().WithMany()
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__variant_v__varia__6C190EBB"),
                    j =>
                    {
                        j.HasKey("VariantId", "ValueId").HasName("PK__variant___DA32A69F25A76C40");
                        j.ToTable("variant_values");
                        j.IndexerProperty<int>("VariantId").HasColumnName("variant_id");
                        j.IndexerProperty<int>("ValueId").HasColumnName("value_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
