using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.ProductTagDTOs;

public class ProductTagUpdateDto
{
    public string Name { get; set; }
    public string? Slug { get; set; }
    public bool IsActive { get; set; }
}