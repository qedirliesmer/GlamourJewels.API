using AutoMapper;
using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.OrderDTOs;
using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class OrderService:IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderResponseDto> CreateOrderAsync(Guid userId, OrderCreateDto dto)
    {
        var order = _mapper.Map<Order>(dto);
        order.UserId = userId;
        await _repository.AddAsync(order);
        return _mapper.Map<OrderResponseDto>(order);
    }

    public async Task<OrderResponseDto> GetOrderByIdAsync(Guid id, Guid currentUserId, string role)
    {
        var order = await _repository.GetByIdAsync(id);

        if (role == "Buyer" && order.UserId != currentUserId)
            throw new UnauthorizedAccessException("You cannot access this order.");

        return _mapper.Map<OrderResponseDto>(order);
    }

    public async Task<List<OrderResponseDto>> GetMyOrdersAsync(Guid userId)
    {
        var orders = await _repository.GetByUserIdAsync(userId);
        return _mapper.Map<List<OrderResponseDto>>(orders);
    }

    public async Task<List<OrderResponseDto>> GetAllOrdersAsync(string role)
    {
        if (role == "Buyer")
            throw new UnauthorizedAccessException("Buyers cannot access all orders.");

        var orders = await _repository.GetAllAsync();
        return _mapper.Map<List<OrderResponseDto>>(orders);
    }

    public async Task<OrderResponseDto> UpdateOrderAsync(Guid id, OrderUpdateDto dto)
    {
        var order = await _repository.GetByIdAsync(id);
        _mapper.Map(dto, order);

        _repository.Update(order);
        await _repository.SaveChangesAsync();

        return _mapper.Map<OrderResponseDto>(order);
    }

    public async Task DeleteOrderAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);

        _repository.Remove(order);
        await _repository.SaveChangesAsync();
    }
}
