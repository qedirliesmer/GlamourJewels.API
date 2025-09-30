using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.ProductImageDTOs;

public class ProductImageCreateDto
{
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string? ThumbnailUrl { get; set; }
    public string? AltText { get; set; }
    public string? Title { get; set; }
    public bool IsMain { get; set; } = false;
    public int DisplayOrder { get; set; } = 0;
}
