using GlamourJewels.Application.DTOs.CartItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Services;

public interface ICartItemService
{
    Task<CartItemResponseDto> CreateAsync(Guid currentUserId, CartItemCreateDto dto);
    Task<CartItemResponseDto> GetByIdAsync(Guid id, Guid currentUserId, string role);
    Task<List<CartItemResponseDto>> GetByCartIdAsync(Guid cartId, Guid currentUserId, string role);
    Task<CartItemResponseDto> UpdateAsync(Guid id, Guid currentUserId, string role, CartItemUpdateDto dto);
    Task DeleteAsync(Guid id, Guid currentUserId, string role);
}