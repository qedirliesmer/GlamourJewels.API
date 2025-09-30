using GlamourJewels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        // Primary Key
        builder.HasKey(r => r.Id);

        // Product əlaqəsi (1:N)
        builder.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // User əlaqəsi (1:N) → bir user-in çox review-u ola bilər
        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Rating
        builder.Property(r => r.Rating)
            .IsRequired();

        // Comment
        builder.Property(r => r.Comment)
            .IsRequired()
            .HasMaxLength(1000);

        // IsApproved default false
        builder.Property(r => r.IsApproved)
            .HasDefaultValue(false);

        // IsAnonymous default false
        builder.Property(r => r.IsAnonymous)
            .HasDefaultValue(false);

        // ApprovedAt - optional
        builder.Property(r => r.ApprovedAt);
    }
}
