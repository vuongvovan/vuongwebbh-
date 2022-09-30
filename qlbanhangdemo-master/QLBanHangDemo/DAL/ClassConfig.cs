using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLBanHangDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo.DAL
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
          //  builder.Property(e => e.ProductId).ValueGeneratedNever();
            builder.Property(e => e.ProductId).ValueGeneratedOnAdd();
            builder.HasOne(d => d.Brand)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_BRAND");

            builder.HasOne(d => d.Category)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_CATEGORY");
        }
    }

    public class CatergoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
             //   builder.Property(e => e.CategoryId).ValueGeneratedNever();
            builder.Property(e => e.CategoryId).ValueGeneratedOnAdd();
        }
    }
    public class CustomerConfig : IEntityTypeConfiguration<Customers>
    {
        public void Configure(EntityTypeBuilder<Customers> builder)
        {
                builder.HasKey(e => e.CustomersId)
                    .HasName("PK_CUSTOMER");

             //   builder.Property(e => e.CustomersId).ValueGeneratedNever();
            builder.Property(e => e.CustomersId).ValueGeneratedOnAdd();
            builder.Property(e => e.CustomersPhone)
                    .IsUnicode(false)
                    .IsFixedLength(true);
        }
    }
    public class BrandConfig : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(e => e.BrandId).ValueGeneratedOnAdd();
            //builder.Property(e => e.BrandId).ValueGeneratedNever();
        }
    }
    public class OrderConfig : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
               // builder.Property(e => e.OrdersId).ValueGeneratedNever();
            builder.Property(e => e.OrdersId).ValueGeneratedOnAdd();
            builder.HasOne(d => d.Customers)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERS_CUSTOMERS");

                builder.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERS_PAYMENT");

        }
    }
    public class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
                builder.HasKey(e => new { e.ProductId, e.OrdersId, e.OrderDetailId })
                    .HasName("PK_ORDERS_DETAIL");

                builder.HasOne(d => d.Orders)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrdersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERS_DETAIL_ORDERS");

                builder.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERS_DETAIL_PRODUCT");

        }
    }
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(e => e.PaymentId).ValueGeneratedOnAdd();
           // builder.Property(e => e.PaymentId).ValueGeneratedNever();
        }
    }
}
