using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.ProductImageDTOs;

public class ProductImageUpdateDto
{
    public string? ImageUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? AltText { get; set; }
    public string? Title { get; set; }
    public bool? IsMain { get; set; }
    public int? DisplayOrder { get; set; }
}