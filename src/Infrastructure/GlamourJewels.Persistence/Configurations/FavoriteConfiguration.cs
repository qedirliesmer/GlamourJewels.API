using GlamourJewels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Configurations;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.ToTable("Favorites");

        // Primary Key
        builder.HasKey(f => f.Id);

        // IsActive default true
        builder.Property(f => f.IsActive)
            .HasDefaultValue(true);

        // FavoritedAt default UTC tarixi
        builder.Property(f => f.FavoritedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        // Məhsul əlaqəsi
        builder.HasOne(f => f.Product)
            .WithMany(p => p.Favorites)
            .HasForeignKey(f => f.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // İstifadəçi əlaqəsi
        //builder.HasOne(f => f.User)
        //    .WithMany(u => u.Favorites)
        //    .HasForeignKey(f => f.UserId)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// Eyni məhsul eyni istifadəçi üçün bir dəfə favorit ola bilər
        //builder.HasIndex(f => new { f.UserId, f.ProductId })
        //       .IsUnique();
    }
}
