using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.ReviewDTOs;
using GlamourJewels.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GlamourJewels.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _service;

    public ReviewsController(IReviewService service)
    {
        _service = service;
    }

    private string CurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    private string CurrentUserRole() => User.FindFirstValue(ClaimTypes.Role);

    // Buyer can create
    [HttpPost]
    [Authorize(Policy = Permissions.ReviewPermissions.Create)]
    public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
    {
        var userId = CurrentUserId();
        var review = await _service.CreateAsync(userId, dto);
        return Ok(review);
    }

    // Get by id — policy View
    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.ReviewPermissions.View)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = CurrentUserId();
        var role = CurrentUserRole();
        var review = await _service.GetByIdAsync(id, userId, role);
        return Ok(review);
    }

    // Get reviews for a product (public: Buyers see only approved; Sellers/Admin see all)
    [HttpGet("product/{productId}")]
    [Authorize] // authentication required; service filters based on role
    public async Task<IActionResult> GetByProduct(Guid productId)
    {
        var userId = CurrentUserId();
        var role = CurrentUserRole();
        var list = await _service.GetByProductIdAsync(productId, userId, role);
        return Ok(list);
    }

    // Get my reviews
    [HttpGet("my")]
    [Authorize(Policy = Permissions.ReviewPermissions.View)]
    public async Task<IActionResult> GetMy()
    {
        var userId = CurrentUserId();
        var list = await _service.GetMyReviewsAsync(userId);
        return Ok(list);
    }

    // Approve/unapprove — Admin/Moderator
    [HttpPost("{id}/approve")]
    [Authorize(Policy = Permissions.ReviewPermissions.Approve)]
    public async Task<IActionResult> Approve(Guid id, [FromBody] ReviewApproveDto dto)
    {
        var userId = CurrentUserId();
        var role = CurrentUserRole();
        var review = await _service.ApproveAsync(id, userId, role, dto.Approve);
        return Ok(review);
    }

    // Delete (Buyer own or Admin)
    [HttpDelete("{id}")]
    [Authorize] // service enforces owner/admin logic
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = CurrentUserId();
        var role = CurrentUserRole();
        await _service.DeleteAsync(id, userId, role);
        return NoContent();
    }
}
