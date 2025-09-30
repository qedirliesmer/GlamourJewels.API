using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.CategoryDTOs;

public class CategoryUpdateDto
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public string MetaTitle { get; set; }
    public string MetaDescription { get; set; }
    public Guid? ParentId { get; set; }
}
