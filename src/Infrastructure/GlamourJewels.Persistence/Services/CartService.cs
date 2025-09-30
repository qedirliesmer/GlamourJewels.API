using AutoMapper;
using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.CartDTOs;
using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class CartService:ICartService
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;

    public CartService(ICartRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CartResponseDto> CreateCartAsync(Guid userId)
    {
        var cart = new Cart { UserId = userId };
        await _repository.AddAsync(cart);
        await _repository.SaveChangesAsync();
        return _mapper.Map<CartResponseDto>(cart);
    }

    public async Task<CartResponseDto> GetByUserIdAsync(Guid userId)
    {
        var cart = await _repository.GetByUserIdAsync(userId);
        if (cart == null) throw new KeyNotFoundException("Cart not found.");
        return _mapper.Map<CartResponseDto>(cart);
    }

    public async Task<CartResponseDto> UpdateCartAsync(Guid id, CartUpdateDto dto)
    {
        var cart = await _repository.GetByIdAsync(id);
        if (cart == null) throw new KeyNotFoundException("Cart not found.");

        _mapper.Map(dto, cart);
        _repository.Update(cart);
        await _repository.SaveChangesAsync();
        return _mapper.Map<CartResponseDto>(cart);
    }

    public async Task DeleteCartAsync(Guid id)
    {
        var cart = await _repository.GetByIdAsync(id);
        if (cart == null) throw new KeyNotFoundException("Cart not found.");

        _repository.Remove(cart);
        await _repository.SaveChangesAsync();
    }
}
