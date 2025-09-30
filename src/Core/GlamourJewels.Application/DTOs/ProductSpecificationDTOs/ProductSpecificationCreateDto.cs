using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.ProductSpecificationDTOs;

public class ProductSpecificationCreateDto
{
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string? Unit { get; set; }
    public Guid ProductId { get; set; }
    public string? SpecificationCategory { get; set; }
}
