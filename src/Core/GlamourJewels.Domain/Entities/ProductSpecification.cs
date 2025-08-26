using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Domain.Entities;

public class ProductSpecification: BaseEntity
{
    public string Key { get; set; }                 // Xüsusiyyətin adı (məsələn: Material)
    public string Value { get; set; }               // Xüsusiyyətin dəyəri (məsələn: Gold)
    public string? Unit { get; set; }               // Ölçü vahidi (məsələn: cm, g)
    public Guid ProductId { get; set; }             // Məhsul əlaqəsi
    public Product Product { get; set; }            // Navigation

    // Yeni xüsusiyyətlər
    public bool IsActive { get; set; } = true;     // Soft delete / passiv xüsusiyyət
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Yaradıcılıq tarixi
    public string? SpecificationCategory { get; set; } // Texniki xüsusiyyət kateqoriyası (Material, Dimensions, Care)
}
