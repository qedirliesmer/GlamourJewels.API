using GlamourJewels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        // Cədvəl adı
        builder.ToTable("ProductImages");

        // Primary Key
        builder.HasKey(pi => pi.Id);

        // ImageUrl
        builder.Property(pi => pi.ImageUrl)
            .IsRequired()
            .HasMaxLength(500);

        // ThumbnailUrl - optional
        builder.Property(pi => pi.ThumbnailUrl)
            .HasMaxLength(500);

        // AltText & Title
        builder.Property(pi => pi.AltText)
            .HasMaxLength(200);

        builder.Property(pi => pi.Title)
            .HasMaxLength(200);

        // IsMain - default false
        builder.Property(pi => pi.IsMain)
            .HasDefaultValue(false);

        // DisplayOrder - default 0
        builder.Property(pi => pi.DisplayOrder)
            .HasDefaultValue(0);

        // Product ilə əlaqə (1:N)
        builder.HasOne(pi => pi.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
