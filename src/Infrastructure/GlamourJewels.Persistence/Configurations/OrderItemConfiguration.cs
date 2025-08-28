using GlamourJewels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        // Cədvəl adı
        builder.ToTable("OrderItems");

        // Primary Key
        builder.HasKey(oi => oi.Id);

        // Quantity
        builder.Property(oi => oi.Quantity)
            .IsRequired();

        // Price - decimal precision
        builder.Property(oi => oi.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // Discount - decimal precision, default 0
        builder.Property(oi => oi.Discount)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        // ProductVariant - optional
        builder.Property(oi => oi.ProductVariant)
            .HasMaxLength(200);

        // Notes - optional
        builder.Property(oi => oi.Notes)
            .HasMaxLength(500);

        // Order ilə əlaqə (1:N)
        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        // Product ilə əlaqə (1:N)
        builder.HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict); // məhsul silinərsə orderitem silinməsin
    }
}
