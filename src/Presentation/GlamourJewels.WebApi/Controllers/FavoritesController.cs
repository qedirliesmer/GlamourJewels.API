using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.FavoriteDTOs;
using GlamourJewels.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GlamourJewels.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoritesController : ControllerBase
{
    private readonly IFavoriteService _service;

    public FavoritesController(IFavoriteService service)
    {
        _service = service;
    }

    private Guid CurrentUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    private string CurrentUserRole() => User.FindFirstValue(ClaimTypes.Role);

    // Buyer əlavə edə bilər
    [HttpPost]
    [Authorize(Policy = Permissions.FavoritePermissions.Create)]
    public async Task<IActionResult> Create([FromBody] FavoriteCreateDto dto)
    {
        var userId = CurrentUserId();
        var fav = await _service.CreateAsync(userId, dto);
        return Ok(fav);
    }

    // Buyer öz favoritlərini götürür
    [HttpGet("my")]
    [Authorize(Policy = Permissions.FavoritePermissions.View)]
    public async Task<IActionResult> GetMy()
    {
        var userId = CurrentUserId();
        var favs = await _service.GetMyFavoritesAsync(userId);
        return Ok(favs);
    }

    // Get by id (Buyer own OR admin/moderator)
    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.FavoritePermissions.View)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = CurrentUserId();
        var role = CurrentUserRole();
        var fav = await _service.GetByIdAsync(id, userId, role);
        return Ok(fav);
    }

    // Delete (Buyer own or Admin)
    [HttpDelete("{id}")]
    [Authorize] // authenticated; service enforces owner/admin logic
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = CurrentUserId();
        var role = CurrentUserRole();
        await _service.DeleteAsync(id, userId, role);
        return NoContent();
    }
}
