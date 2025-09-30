using GlamourJewels.Application.DTOs.OrderItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Services;

public interface IOrderItemService
{
    Task<OrderItemResponseDto> CreateAsync(OrderItemCreateDto dto);
    Task<OrderItemResponseDto> GetByIdAsync(Guid id);
    Task<List<OrderItemResponseDto>> GetByOrderIdAsync(Guid orderId);
    Task<OrderItemResponseDto> UpdateAsync(Guid id, OrderItemUpdateDto dto);
    Task DeleteAsync(Guid id);
}
