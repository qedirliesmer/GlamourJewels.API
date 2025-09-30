using GlamourJewels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        // Primary Key
        builder.HasKey(o => o.Id);

        // TotalAmount - decimal precision
        builder.Property(o => o.TotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // Status - default Pending
        builder.Property(o => o.Status)
            .HasMaxLength(50)
            .HasDefaultValue("Pending");

        // ShippingAddress & BillingAddress
        builder.Property(o => o.ShippingAddress)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(o => o.BillingAddress)
            .HasMaxLength(500)
            .IsRequired();

        // PaymentMethod
        builder.Property(o => o.PaymentMethod)
            .HasMaxLength(50)
            .HasDefaultValue("CashOnDelivery");

        // IsPaid
        builder.Property(o => o.IsPaid)
            .HasDefaultValue(false);

        // OrderDate
        builder.Property(o => o.OrderDate)
            .HasDefaultValueSql("GETUTCDATE()");

        // OrderItems ilə əlaqə (1:N)
        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(o => o.User)
             .WithMany(u => u.Orders)
             .HasForeignKey(o => o.UserId)
             .OnDelete(DeleteBehavior.Restrict);

    }
}
