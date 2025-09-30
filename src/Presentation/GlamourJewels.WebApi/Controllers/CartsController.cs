using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.CartDTOs;
using GlamourJewels.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;

namespace GlamourJewels.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICartService _service;

    public CartsController(ICartService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Policy = "Cart.Create")] // Buyer
    public async Task<IActionResult> Create()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var cart = await _service.CreateCartAsync(userId);
        return Ok(cart);
    }

    [HttpGet("me")]
    [Authorize(Policy = "Cart.View")] // Buyer
    public async Task<IActionResult> GetMyCart()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var cart = await _service.GetByUserIdAsync(userId);
        return Ok(cart);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "Cart.Update")] // Admin
    public async Task<IActionResult> Update(Guid id, [FromBody] CartUpdateDto dto)
    {
        var cart = await _service.UpdateCartAsync(id, dto);
        return Ok(cart);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Cart.Delete")] // Admin
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteCartAsync(id);
        return NoContent();
    }
}

