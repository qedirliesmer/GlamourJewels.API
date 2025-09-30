using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.ProductSpecificationDTOs;

public class ProductSpecificationUpdateDto
{
    public string? Key { get; set; }
    public string? Value { get; set; }
    public string? Unit { get; set; }
    public string? SpecificationCategory { get; set; }
}
