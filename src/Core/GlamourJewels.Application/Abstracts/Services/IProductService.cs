using GlamourJewels.Application.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Services;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAllAsync();
    Task<ProductResponseDto?> GetByIdAsync(Guid id);
    Task<ProductResponseDto> CreateAsync(ProductCreateDto dto, string userId);
    Task<ProductResponseDto?> UpdateAsync(Guid id, ProductUpdateDto dto, string userId);
    Task<bool> DeleteAsync(Guid id, string userId);
}
