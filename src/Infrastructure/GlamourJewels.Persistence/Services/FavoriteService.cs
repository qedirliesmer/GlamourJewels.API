using AutoMapper;
using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.FavoriteDTOs;
using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _repo;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepo; // to validate product exists (optional)

    public FavoriteService(IFavoriteRepository repo, IMapper mapper, IProductRepository productRepo)
    {
        _repo = repo;
        _mapper = mapper;
        _productRepo = productRepo;
    }

    public async Task<FavoriteResponseDto> CreateAsync(Guid currentUserId, FavoriteCreateDto dto)
    {
        // check product exists (optional but recommended)
        var product = await _productRepo.GetByIdAsync(dto.ProductId); // will throw if not exists

        // check existing favorite
        var existing = await _repo.GetByUserAndProductAsync(currentUserId, dto.ProductId);
        if (existing != null)
        {
            if (existing.IsActive)
            {
                return _mapper.Map<FavoriteResponseDto>(existing); // already favorited
            }
            // reactivate
            existing.IsActive = true;
            existing.FavoritedAt = DateTime.UtcNow;
            _repo.Update(existing);
            await _repo.SaveChangesAsync();
            return _mapper.Map<FavoriteResponseDto>(existing);
        }

        var entity = new Favorite
        {
            UserId = currentUserId,
            ProductId = dto.ProductId,
            IsActive = true,
            FavoritedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(entity); // add + save
        return _mapper.Map<FavoriteResponseDto>(entity);
    }

    public async Task<List<FavoriteResponseDto>> GetMyFavoritesAsync(Guid currentUserId)
    {
        var favs = await _repo.GetByUserIdAsync(currentUserId);
        return _mapper.Map<List<FavoriteResponseDto>>(favs);
    }

    public async Task<FavoriteResponseDto> GetByIdAsync(Guid id, Guid currentUserId, string role)
    {
        var fav = await _repo.GetByIdAsync(id);

        if (role == "Buyer" && fav.UserId != currentUserId)
            throw new UnauthorizedAccessException("You cannot access this favorite.");

        return _mapper.Map<FavoriteResponseDto>(fav);
    }

    public async Task DeleteAsync(Guid id, Guid currentUserId, string role)
    {
        var fav = await _repo.GetByIdAsync(id);

        // Buyer can delete own (soft remove by setting IsActive=false), Admin can delete any
        if (role == "Buyer" && fav.UserId != currentUserId)
            throw new UnauthorizedAccessException("You cannot delete this favorite.");

        // If you prefer soft-delete:
        fav.IsActive = false;
        _repo.Update(fav);
        await _repo.SaveChangesAsync();

        // If you prefer hard delete:
        // _repo.Remove(fav);
        // await _repo.SaveChangesAsync();
    }
}