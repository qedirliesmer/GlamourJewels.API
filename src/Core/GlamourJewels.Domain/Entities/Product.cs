using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Domain.Entities;

public class Product:BaseEntity
{
    public string? Name { get; set; }
    public string? Slug { get; set; } // SEO üçün URL-friendly ad
    public string? Description { get; set; }
    public string? ShortDescription { get; set; } // qısa təsvir
    public decimal Price { get; set; } = 0;
    public decimal? DiscountPrice { get; set; } // endirim qiyməti
    public int Stock { get; set; }
    public bool IsActive { get; set; } = true; // məhsul aktiv/passiv

    // SEO dəstəyi
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }

    // Əlaqələr
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    // Navigasiyalar
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    // Yeni gələcək funksiyalar üçün
    public ICollection<ProductTag> Tags { get; set; } = new List<ProductTag>(); // məhsul teqləri
    public ICollection<ProductSpecification> Specifications { get; set; } = new List<ProductSpecification>(); // texniki xüsusiyyətlər
}