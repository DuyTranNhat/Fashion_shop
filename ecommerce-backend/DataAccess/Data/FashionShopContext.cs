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

    public virtual DbSet<AttributeValue> AttributeValues { get; set; }

    public virtual DbSet<CampaignProduct> CampaignProducts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<MarketingCampaign> MarketingCampaigns { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

    public virtual DbSet<ProductReview> ProductReviews { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<VariantAttribute> VariantAttributes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttributeValue>(entity =>
        {
            entity.HasKey(e => e.ValueId).HasName("PK__attribut__0FECE282C8507406");

            entity.ToTable("attribute_value");

            entity.Property(e => e.ValueId)
                .ValueGeneratedNever()
                .HasColumnName("value_id");
            entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeValues)
                .HasForeignKey(d => d.AttributeId)
                .HasConstraintName("FK__attribute__attri__45F365D3");
        });

        modelBuilder.Entity<CampaignProduct>(entity =>
        {
            entity.HasKey(e => e.CampaignProductId).HasName("PK__campaign__004B16482091492E");

            entity.ToTable("campaign_products");

            entity.Property(e => e.CampaignProductId).HasColumnName("campaign_product_id");
            entity.Property(e => e.CampaignId).HasColumnName("campaign_id");
            entity.Property(e => e.DiscountPercentage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("discount_percentage");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.SlideDescription)
                .HasMaxLength(255)
                .HasColumnName("slide_description");
            entity.Property(e => e.SlideImage)
                .HasMaxLength(255)
                .HasColumnName("slide_image");
            entity.Property(e => e.SlideTitle)
                .HasMaxLength(255)
                .HasColumnName("slide_title");
            entity.Property(e => e.SlideUrl)
                .HasMaxLength(255)
                .HasColumnName("slide_url");

            entity.HasOne(d => d.Campaign).WithMany(p => p.CampaignProducts)
                .HasForeignKey(d => d.CampaignId)
                .HasConstraintName("FK__campaign___campa__4BAC3F29");

            entity.HasOne(d => d.Product).WithMany(p => p.CampaignProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__campaign___produ__4CA06362");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__categori__D54EE9B4B3B711E1");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK__categorie__paren__4316F928");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB8524DD540E");

            entity.ToTable("customers");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__images__DC9AC95530D37B39");

            entity.ToTable("images");

            entity.Property(e => e.ImageId)
                .ValueGeneratedNever()
                .HasColumnName("image_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.VariantId).HasColumnName("variant_id");

            entity.HasOne(d => d.Variant).WithMany(p => p.Images)
                .HasForeignKey(d => d.VariantId)
                .HasConstraintName("FK__images__variant___44FF419A");
        });

        modelBuilder.Entity<MarketingCampaign>(entity =>
        {
            entity.HasKey(e => e.CampaignId).HasName("PK__marketin__905B681C0BD6E193");

            entity.ToTable("marketing_campaigns");

            entity.Property(e => e.CampaignId).HasColumnName("campaign_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__46596229E8D410FC");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(255)
                .HasColumnName("payment_method");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.ShippingService)
                .HasMaxLength(255)
                .HasColumnName("shipping_service");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__orders__customer__48CFD27E");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__order_de__3C5A4080375D12C5");

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
                .HasConstraintName("FK__order_det__order__49C3F6B7");

            entity.HasOne(d => d.Variant).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.VariantId)
                .HasConstraintName("FK__order_det__varia__4AB81AF0");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__47027DF50D199329");

            entity.ToTable("products");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__products__catego__412EB0B6");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__products__suppli__4222D4EF");
        });

        modelBuilder.Entity<ProductAttribute>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PK__product___9090C9BB6E510AC1");

            entity.ToTable("product_attributes");

            entity.Property(e => e.AttributeId)
                .ValueGeneratedNever()
                .HasColumnName("attribute_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__product___60883D902E8A8437");

            entity.ToTable("product_reviews");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReviewDate)
                .HasColumnType("datetime")
                .HasColumnName("review_date");

            entity.HasOne(d => d.Customer).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__product_r__custo__4E88ABD4");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__product_r__produ__4D94879B");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(e => e.VariantId).HasName("PK__product___EACC68B732CF6E5C");

            entity.ToTable("product_variants");

            entity.Property(e => e.VariantId).HasColumnName("variant_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.VariantName)
                .HasMaxLength(255)
                .HasColumnName("variant_name");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__product_v__produ__440B1D61");
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("PK__receipts__91F52C1FD8FD5AF5");

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
            entity.HasKey(e => new { e.ReceiptId, e.VariantId }).HasName("PK__receipt___FF59EA94B4C7A75A");

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
                .HasConstraintName("FK__receipt_d__recei__4F7CD00D");

            entity.HasOne(d => d.Variant).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.VariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__receipt_d__varia__5070F446");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__supplier__6EE594E87031506A");

            entity.ToTable("suppliers");

            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<VariantAttribute>(entity =>
        {
            entity.HasKey(e => e.VariantAttributeId).HasName("PK__variant___4B8BB8B047B8D90E");

            entity.ToTable("variant_attributes");

            entity.Property(e => e.VariantAttributeId).HasColumnName("variant_attribute_id");
            entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
            entity.Property(e => e.VariantId).HasColumnName("variant_id");

            entity.HasOne(d => d.Attribute).WithMany(p => p.VariantAttributes)
                .HasForeignKey(d => d.AttributeId)
                .HasConstraintName("FK__variant_a__attri__47DBAE45");

            entity.HasOne(d => d.Variant).WithMany(p => p.VariantAttributes)
                .HasForeignKey(d => d.VariantId)
                .HasConstraintName("FK__variant_a__varia__46E78A0C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
