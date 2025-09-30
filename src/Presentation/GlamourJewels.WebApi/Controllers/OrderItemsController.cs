using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.OrderItemDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GlamourJewels.Application.Shared.Permissions;

namespace GlamourJewels.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderItemsController : ControllerBase
{
    private readonly IOrderItemService _service;

    public OrderItemsController(IOrderItemService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Policy = OrderPermissions.Create)] // Buyer
    public async Task<IActionResult> Create([FromBody] OrderItemCreateDto dto)
    {
        var item = await _service.CreateAsync(dto);
        return Ok(item);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = OrderPermissions.ViewById)] // Buyer(own), Seller, Admin, Moderator
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        return Ok(item);
    }

    [HttpGet("byorder/{orderId}")]
    [Authorize(Policy = OrderPermissions.ViewById)] // Buyer(own), Seller, Admin, Moderator
    public async Task<IActionResult> GetByOrderId(Guid orderId)
    {
        var items = await _service.GetByOrderIdAsync(orderId);
        return Ok(items);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = OrderPermissions.Update)] // Admin
    public async Task<IActionResult> Update(Guid id, [FromBody] OrderItemUpdateDto dto)
    {
        var item = await _service.UpdateAsync(id, dto);
        return Ok(item);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = OrderPermissions.Delete)] // Admin
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
