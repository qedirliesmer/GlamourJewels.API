using GlamourJewels.Application.DTOs.ProductSpecificationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Services;

public interface IProductSpecificationService
{
    Task<ProductSpecificationResponseDto> CreateAsync(ProductSpecificationCreateDto dto);
    Task<ProductSpecificationResponseDto> UpdateAsync(Guid id, ProductSpecificationUpdateDto dto);
    Task DeleteAsync(Guid id);
    Task<ProductSpecificationResponseDto> GetByIdAsync(Guid id);
    Task<List<ProductSpecificationResponseDto>> GetByProductIdAsync(Guid productId);
}

