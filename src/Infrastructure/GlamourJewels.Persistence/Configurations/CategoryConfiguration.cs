using GlamourJewels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        // Primary Key
        builder.HasKey(c => c.Id);

        // Name - mütləq və maksimum uzunluq
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(150);

        // Slug - SEO üçün unikal URL-friendly ad
        builder.Property(c => c.Slug)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(c => c.Slug)
            .IsUnique();

        // Description - optional
        builder.Property(c => c.Description)
            .HasMaxLength(500);

        // ImageUrl - optional, maksimum uzunluq
        builder.Property(c => c.ImageUrl)
            .HasMaxLength(500);

        // IsActive - default true
        builder.Property(c => c.IsActive)
            .HasDefaultValue(true);

        // MetaTitle - SEO üçün
        builder.Property(c => c.MetaTitle)
            .HasMaxLength(150);

        // MetaDescription - SEO üçün
        builder.Property(c => c.MetaDescription)
            .HasMaxLength(300);

        // Nested Categories (Öz-özünə əlaqə)
        builder.HasOne(c => c.Parent)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Məhsullarla əlaqə (1:N) - bir kateqoriyada çox məhsul ola bilər
        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
