using GlamourJewels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Configurations;

public class ProductSpecificationConfiguration : IEntityTypeConfiguration<ProductSpecification>
{
    public void Configure(EntityTypeBuilder<ProductSpecification> builder)
    {
        // Cədvəl adı
        builder.ToTable("ProductSpecifications");

        // Primary Key
        builder.HasKey(ps => ps.Id);

        // Key & Value
        builder.Property(ps => ps.Key)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(ps => ps.Value)
            .IsRequired()
            .HasMaxLength(500);

        // Unit - optional
        builder.Property(ps => ps.Unit)
            .HasMaxLength(50);

        // IsActive - default true
        builder.Property(ps => ps.IsActive)
            .HasDefaultValue(true);

        // CreatedAt - default current time
        builder.Property(ps => ps.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        // SpecificationCategory - optional
        builder.Property(ps => ps.SpecificationCategory)
            .HasMaxLength(100);

        // Product əlaqəsi (1:N)
        builder.HasOne(ps => ps.Product)
            .WithMany(p => p.Specifications)
            .HasForeignKey(ps => ps.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
