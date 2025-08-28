using GlamourJewels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        // Cədvəl adı
        builder.ToTable("CartItems");

        // Primary Key
        builder.HasKey(ci => ci.Id);

        // Quantity - məhsul sayı, mütləq doldurulmalıdır və mənfi ola bilməz
        builder.Property(ci => ci.Quantity)
            .IsRequired()
            .HasDefaultValue(1);

        // Price - məhsulun həmin anki qiyməti, decimal precision ilə
        builder.Property(ci => ci.Price)
            .HasColumnType("decimal(18,2)") // Böyük layihələrdə daha düzgün format
            .IsRequired();

        // IsSelected - default true
        builder.Property(ci => ci.IsSelected)
            .HasDefaultValue(true);

        // Cart ilə əlaqə (1:N) - bir Cart-da çoxlu CartItem ola bilər
        builder.HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Restrict);

        // Product ilə əlaqə (1:N) - bir məhsul çox CartItem-da ola bilər
        builder.HasOne(ci => ci.Product)
            .WithMany()
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Unikal constraint: bir Cart-da eyni məhsuldan yalnız bir dəfə ola bilər
        builder.HasIndex(ci => new { ci.CartId, ci.ProductId })
            .IsUnique();
    }
}
