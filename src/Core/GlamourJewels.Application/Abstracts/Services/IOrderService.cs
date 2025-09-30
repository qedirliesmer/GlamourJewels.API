using GlamourJewels.Application.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Services;

public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(Guid userId, OrderCreateDto dto);
    Task<OrderResponseDto> GetOrderByIdAsync(Guid id, Guid currentUserId, string role);
    Task<List<OrderResponseDto>> GetMyOrdersAsync(Guid userId);
    Task<List<OrderResponseDto>> GetAllOrdersAsync(string role);
    Task<OrderResponseDto> UpdateOrderAsync(Guid id, OrderUpdateDto dto);
    Task DeleteOrderAsync(Guid id);
}
