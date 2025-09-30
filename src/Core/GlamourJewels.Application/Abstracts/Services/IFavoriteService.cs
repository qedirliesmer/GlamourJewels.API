using GlamourJewels.Application.DTOs.FavoriteDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Services;

public interface IFavoriteService
{
    Task<FavoriteResponseDto> CreateAsync(Guid currentUserId, FavoriteCreateDto dto);
    Task<List<FavoriteResponseDto>> GetMyFavoritesAsync(Guid currentUserId);
    Task<FavoriteResponseDto> GetByIdAsync(Guid id, Guid currentUserId, string role);
    Task DeleteAsync(Guid id, Guid currentUserId, string role);
}