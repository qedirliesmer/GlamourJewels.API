using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Domain.Entities;

public class Category: BaseEntity
{
    public string Name { get; set; }
    public string Slug { get; set; } // SEO üçün URL-friendly ad
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;

    // SEO meta məlumatları
    public string MetaTitle { get; set; }
    public string MetaDescription { get; set; }

    // Öz-özünə əlaqə (nested kateqoriyalar üçün)
    public Guid? ParentId { get; set; }
    public Category? Parent { get; set; }
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();

    // Məhsullar
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
