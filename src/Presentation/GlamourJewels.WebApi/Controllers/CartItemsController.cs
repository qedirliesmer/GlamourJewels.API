using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.CartItemDTOs;
using GlamourJewels.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GlamourJewels.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartItemsController : ControllerBase
{
    private readonly ICartItemService _service;

    public CartItemsController(ICartItemService service)
    {
        _service = service;
    }

    private Guid CurrentUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    private string CurrentUserRole() => User.FindFirstValue(ClaimTypes.Role);

    // Buyer əlavə edə bilər (Cart.Create policy)
    [HttpPost]
    [Authorize(Policy = nameof(Permissions.CartPermissions.Create))] // or "Cart.Create"
    public async Task<IActionResult> Create([FromBody] CartItemCreateDto dto)
    {
        var userId = CurrentUserId();
        var item = await _service.CreateAsync(userId, dto);
        return Ok(item);
    }

    // Viewer: Buyer (own), Admin, Moderator (Cart.View policy)
    [HttpGet("{id}")]
    [Authorize(Policy = nameof(Permissions.CartPermissions.View))] // or "Cart.View"
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = CurrentUserId();
        var role = CurrentUserRole();
        var item = await _service.GetByIdAsync(id, userId, role);
        return Ok(item);
    }

    // Get all items for a cart (owner-only or admin/moderator)
    [HttpGet("bycart/{cartId}")]
    [Authorize(Policy = nameof(Permissions.CartPermissions.View))]
    public async Task<IActionResult> GetByCartId(Guid cartId)
    {
        var userId = CurrentUserId();
        var role = CurrentUserRole();
        var items = await _service.GetByCartIdAsync(cartId, userId, role);
        return Ok(items);
    }

    // Update: authenticated — service enforces owner/admin logic
    [HttpPut("{id}")]
    [Authorize] // any authenticated; service checks role+ownership
    public async Task<IActionResult> Update(Guid id, [FromBody] CartItemUpdateDto dto)
    {
        var userId = CurrentUserId();
        var role = CurrentUserRole();
        var item = await _service.UpdateAsync(id, userId, role, dto);
        return Ok(item);
    }

    // Delete: authenticated — service enforces owner/admin logic
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = CurrentUserId();
        var role = CurrentUserRole();
        await _service.DeleteAsync(id, userId, role);
        return NoContent();
    }
}
