using AutoMapper;
using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.ReviewDTOs;
using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repo;
    private readonly IProductRepository _productRepo; // optional, to validate product
    private readonly IMapper _mapper;

    public ReviewService(IReviewRepository repo, IProductRepository productRepo, IMapper mapper)
    {
        _repo = repo;
        _productRepo = productRepo;
        _mapper = mapper;
    }

    public async Task<ReviewResponseDto> CreateAsync(string currentUserId, ReviewCreateDto dto)
    {
        // validate product exists (optional but recommended)
        var product = await _productRepo.GetByIdAsync(dto.ProductId); // throws if not found

        var review = new Review
        {
            ProductId = dto.ProductId,
            UserId = currentUserId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            IsAnonymous = dto.IsAnonymous,
            IsApproved = false,
            ApprovedAt = null
            // CreatedAt handled by BaseEntity if present
        };

        await _repo.AddAsync(review);

        // map result
        var result = _mapper.Map<ReviewResponseDto>(review);
        // optionally set UserName null if anonymous
        if (review.IsAnonymous) result.UserName = null;
        return result;
    }

    public async Task<ReviewResponseDto> GetByIdAsync(Guid id, string currentUserId, string role)
    {
        var review = await _repo.GetByIdAsync(id);

        // buyer can only view own reviews unless approved/public; but business said Buyer sees own
        if (role == "Buyer" && review.UserId != currentUserId)
        {
            // sellers can view reviews of their products - will be allowed below
            throw new UnauthorizedAccessException("You cannot access this review.");
        }

        // Sellers: allow if it's their product
        if (role == "Seller")
        {
            // product.UserId (seller owner) check:
            if (review.Product.AppUserId != currentUserId)
                throw new UnauthorizedAccessException("You cannot access this review.");
        }

        return _mapper.Map<ReviewResponseDto>(review);
    }

    public async Task<List<ReviewResponseDto>> GetByProductIdAsync(Guid productId, string currentUserId, string role)
    {
        // Sellers and admin/moderator and public viewers:
        // If Buyer requests reviews for a product, allow (publicly visible) — but for unapproved reviews, maybe only admin/moderator/seller can see them.
        var reviews = await _repo.GetByProductIdAsync(productId);

        // Apply business rule: If buyer and we want only approved reviews, filter
        if (role == "Buyer")
            reviews = reviews.Where(r => r.IsApproved).ToList();

        // If Seller: allow but restrict? seller sees all reviews of his product only
        if (role == "Seller")
        {
            // check product owner
            // need product to check owner id
            var prod = await _productRepo.GetByIdAsync(productId);
            if (prod.AppUserId != currentUserId)
                throw new UnauthorizedAccessException("You cannot access reviews for this product.");
        }

        return _mapper.Map<List<ReviewResponseDto>>(reviews);
    }

    public async Task<List<ReviewResponseDto>> GetMyReviewsAsync(string currentUserId)
    {
        var list = await _repo.GetByUserIdAsync(currentUserId);
        return _mapper.Map<List<ReviewResponseDto>>(list);
    }

    public async Task<ReviewResponseDto> ApproveAsync(Guid id, string currentUserId, string role, bool approve)
    {
        var review = await _repo.GetByIdAsync(id);

        // only Admin or Moderator can approve
        if (role != "Admin" && role != "Moderator")
            throw new UnauthorizedAccessException("Only Admin or Moderator can approve reviews.");

        review.IsApproved = approve;
        review.ApprovedAt = approve ? DateTime.UtcNow : null;
        _repo.Update(review);
        await _repo.SaveChangesAsync();

        return _mapper.Map<ReviewResponseDto>(review);
    }

    public async Task DeleteAsync(Guid id, string currentUserId, string role)
    {
        var review = await _repo.GetByIdAsync(id);

        // Buyer can delete own review; Admin can delete any
        if (role == "Buyer" && review.UserId != currentUserId)
            throw new UnauthorizedAccessException("You cannot delete this review.");

        // Sellers cannot delete reviews (unless admin)
        _repo.Remove(review);
        await _repo.SaveChangesAsync();
    }
}