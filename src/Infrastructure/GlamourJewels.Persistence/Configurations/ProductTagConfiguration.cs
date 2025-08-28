using GlamourJewels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Configurations;
public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
{
    public void Configure(EntityTypeBuilder<ProductTag> builder)
    {
        builder.ToTable("ProductTags");

        // Primary Key
        builder.HasKey(pt => pt.Id);

        // Name
        builder.Property(pt => pt.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Slug - optional
        builder.Property(pt => pt.Slug)
            .HasMaxLength(150);

        // IsActive - default true
        builder.Property(pt => pt.IsActive)
            .HasDefaultValue(true);

        // CreatedAt - default current time
        builder.Property(pt => pt.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        // Product ilə əlaqə (1:N)
        builder.HasOne(pt => pt.Product)
            .WithMany(p => p.Tags)
            .HasForeignKey(pt => pt.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
