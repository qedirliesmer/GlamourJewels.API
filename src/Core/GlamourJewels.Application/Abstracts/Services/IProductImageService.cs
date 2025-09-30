using GlamourJewels.Application.DTOs.ProductImageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Services;

public interface IProductImageService
{
    Task<ProductImageResponseDto> CreateAsync(ProductImageCreateDto dto);
    Task<ProductImageResponseDto> UpdateAsync(Guid id, ProductImageUpdateDto dto);
    Task DeleteAsync(Guid id);
    Task<List<ProductImageResponseDto>> GetByProductIdAsync(Guid productId);
}