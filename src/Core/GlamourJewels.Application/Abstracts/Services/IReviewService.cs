using GlamourJewels.Application.DTOs.ReviewDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Services;

public interface IReviewService
{
    Task<ReviewResponseDto> CreateAsync(string currentUserId, ReviewCreateDto dto);
    Task<ReviewResponseDto> GetByIdAsync(Guid id, string currentUserId, string role);
    Task<List<ReviewResponseDto>> GetByProductIdAsync(Guid productId, string currentUserId, string role);
    Task<List<ReviewResponseDto>> GetMyReviewsAsync(string currentUserId);
    Task<ReviewResponseDto> ApproveAsync(Guid id, string currentUserId, string role, bool approve);
    Task DeleteAsync(Guid id, string currentUserId, string role);
}
