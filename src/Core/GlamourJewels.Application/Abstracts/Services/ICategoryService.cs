using GlamourJewels.Application.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Services;

public interface ICategoryService
{
    Task<CategoryResponseDto> GetByIdAsync(Guid id);
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto);
    Task<CategoryResponseDto> UpdateAsync(Guid id, CategoryUpdateDto dto);
    Task DeleteAsync(Guid id);
}
