using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Domain.Entities;

public class ProductTag:BaseEntity
{
    public string Name { get; set; }                // Tag adı
    public string? Slug { get; set; }               // SEO üçün URL-friendly adı
    public Guid ProductId { get; set; }             // Məhsul əlaqəsi
    public Product Product { get; set; }            // Navigation

    // Yeni xüsusiyyətlər
    public bool IsActive { get; set; } = true;     // Soft delete / passiv tag
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Yaradıcılıq tarixi
}
