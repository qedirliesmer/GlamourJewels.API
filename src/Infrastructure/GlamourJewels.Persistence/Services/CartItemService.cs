using AutoMapper;
using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.CartItemDTOs;
using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class CartItemService : ICartItemService
{
    private readonly ICartItemRepository _repo;
    private readonly ICartRepository _cartRepo; // mövcud olduğunu varsayıram (CartRepository)
    private readonly IMapper _mapper;

    public CartItemService(ICartItemRepository repo, ICartRepository cartRepo, IMapper mapper)
    {
        _repo = repo;
        _cartRepo = cartRepo;
        _mapper = mapper;
    }

    public async Task<CartItemResponseDto> CreateAsync(Guid currentUserId, CartItemCreateDto dto)
    {
        // 1) istifadəçinin cart-ını al (yoxdursa yarat)
        var cart = await _cartRepo.GetByUserIdAsync(currentUserId);
        if (cart == null)
        {
            cart = new Cart { UserId = currentUserId };
            await _cartRepo.AddAsync(cart);
            await _cartRepo.SaveChangesAsync();
        }

        // 2) eyni məhsul varsa quantity artır, yoxsa yeni item əlavə et
        var existing = await _repo.GetByCartAndProductAsync(cart.Id, dto.ProductId);
        if (existing != null)
        {
            existing.Quantity += dto.Quantity;
            existing.Price = dto.Price; // biznes qaidəsi: override edilə bilər
            _repo.Update(existing);
            await _repo.SaveChangesAsync();
            return _mapper.Map<CartItemResponseDto>(existing);
        }

        var entity = _mapper.Map<CartItem>(dto);
        entity.CartId = cart.Id;
        await _repo.AddAsync(entity);

        // AddAsync çağırdıqda SaveChangesAsync repository içində çağırılır
        return _mapper.Map<CartItemResponseDto>(entity);
    }

    public async Task<CartItemResponseDto> GetByIdAsync(Guid id, Guid currentUserId, string role)
    {
        var entity = await _repo.GetByIdAsync(id);

        // owner check: buyer only own
        if (role == "Buyer" && entity.Cart.UserId != currentUserId)
            throw new UnauthorizedAccessException("You cannot access this cart item.");

        return _mapper.Map<CartItemResponseDto>(entity);
    }

    public async Task<List<CartItemResponseDto>> GetByCartIdAsync(Guid cartId, Guid currentUserId, string role)
    {
        var cart = await _cartRepo.GetByIdAsync(cartId);
        if (cart == null) throw new KeyNotFoundException("Cart not found.");

        if (role == "Buyer" && cart.UserId != currentUserId)
            throw new UnauthorizedAccessException("You cannot access this cart items.");

        var items = await _repo.GetByCartIdAsync(cartId);
        return _mapper.Map<List<CartItemResponseDto>>(items);
    }

    public async Task<CartItemResponseDto> UpdateAsync(Guid id, Guid currentUserId, string role, CartItemUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);

        // authorization: buyer can update own; admin can update any
        if (role == "Buyer" && entity.Cart.UserId != currentUserId)
            throw new UnauthorizedAccessException("You cannot update this cart item.");

        // map only non-null fields
        _mapper.Map(dto, entity);

        _repo.Update(entity);
        await _repo.SaveChangesAsync();
        return _mapper.Map<CartItemResponseDto>(entity);
    }

    public async Task DeleteAsync(Guid id, Guid currentUserId, string role)
    {
        var entity = await _repo.GetByIdAsync(id);

        if (role == "Buyer" && entity.Cart.UserId != currentUserId)
            throw new UnauthorizedAccessException("You cannot delete this cart item.");

        _repo.Remove(entity);
        await _repo.SaveChangesAsync();
    }
}