using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Domain.Entities;

public class ProductImage:BaseEntity
{
    public string ImageUrl { get; set; }

    // Thumbnail dəstəyi (performans üçün)
    public string? ThumbnailUrl { get; set; }

    // SEO üçün alt mətn və başlıq
    public string? AltText { get; set; }
    public string? Title { get; set; }

    // Əsas şəkil olub-olmadığı
    public bool IsMain { get; set; } = false;

    // Şəkillərin sıralaması
    public int DisplayOrder { get; set; } = 0;

    // Məhsul əlaqəsi
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
