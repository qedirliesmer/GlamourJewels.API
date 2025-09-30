using GlamourJewels.Application.DTOs.CartDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Services;

public interface ICartService
{
    Task<CartResponseDto> CreateCartAsync(Guid userId);
    Task<CartResponseDto> GetByUserIdAsync(Guid userId);
    Task<CartResponseDto> UpdateCartAsync(Guid id, CartUpdateDto dto);
    Task DeleteCartAsync(Guid id);
}
