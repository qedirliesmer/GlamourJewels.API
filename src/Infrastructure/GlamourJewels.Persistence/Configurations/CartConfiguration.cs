using GlamourJewels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            // Table adı
            builder.ToTable("Carts");

            // Primary Key
            builder.HasKey(c => c.Id);

            // IsActive - default olaraq true
            builder.Property(c => c.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            // TotalAmount - decimal üçün dəqiq precision verək
            builder.Property(c => c.TotalAmount)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            // Bir Cart çoxlu CartItem-a sahib ola bilər
            builder.HasMany(c => c.CartItems)
                   .WithOne(ci => ci.Cart)
                   .HasForeignKey(ci => ci.CartId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Əgər gələcəkdə AppUser əlavə olunacaqsa (şərhə alınmış hissə)
            // builder.HasOne(c => c.User)
            //        .WithOne(u => u.Cart)
            //        .HasForeignKey<Cart>(c => c.UserId)
            //        .OnDelete(DeleteBehavior.Restrict);
        }
 }

