using AutoMapper;
using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.OrderItemDTOs;
using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class OrderItemService:IOrderItemService
{
    private readonly IOrderItemRepository _repository;
    private readonly IMapper _mapper;

    public OrderItemService(IOrderItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderItemResponseDto> CreateAsync(OrderItemCreateDto dto)
    {
        var entity = _mapper.Map<OrderItem>(dto);
        await _repository.AddAsync(entity);
        return _mapper.Map<OrderItemResponseDto>(entity);
    }

    public async Task<OrderItemResponseDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<OrderItemResponseDto>(entity);
    }

    public async Task<List<OrderItemResponseDto>> GetByOrderIdAsync(Guid orderId)
    {
        var entities = await _repository.GetByOrderIdAsync(orderId);
        return _mapper.Map<List<OrderItemResponseDto>>(entities);
    }

    public async Task<OrderItemResponseDto> UpdateAsync(Guid id, OrderItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        _mapper.Map(dto, entity);
        _repository.Update(entity);
        await _repository.SaveChangesAsync();
        return _mapper.Map<OrderItemResponseDto>(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        _repository.Remove(entity);
        await _repository.SaveChangesAsync();
    }
}
