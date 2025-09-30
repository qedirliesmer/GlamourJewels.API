using GlamourJewels.Application.DTOs.ProductTagDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Services;

public interface IProductTagService
{
    Task<ProductTagResponseDto> CreateAsync(Guid productId, ProductTagCreateDto dto);
    Task<ProductTagResponseDto> UpdateAsync(Guid id, ProductTagUpdateDto dto);
    Task DeleteAsync(Guid id);
    Task<List<ProductTagResponseDto>> GetAllByProductIdAsync(Guid productId);
    Task<ProductTagResponseDto> GetByIdAsync(Guid id);
}
