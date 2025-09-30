using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.OrderDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GlamourJewels.Application.Shared.Permissions;
using System.Security.Claims;

namespace GlamourJewels.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    // Utility metodlar
    private Guid CurrentUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    private string CurrentUserRole() => User.FindFirstValue(ClaimTypes.Role);

    // Buyer sifariş yarada bilər
    [HttpPost]
    [Authorize(Policy = OrderPermissions.Create)]
    public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
    {
        var userId = CurrentUserId();
        var order = await _service.CreateOrderAsync(userId, dto);
        return Ok(order);
    }

    // Hər kəs öz icazəsinə görə sifariş görə bilər
    [HttpGet("{id}")]
    [Authorize(Policy = OrderPermissions.ViewById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = CurrentUserId();
        var role = CurrentUserRole();
        var order = await _service.GetOrderByIdAsync(id, userId, role);
        return Ok(order);
    }

    // Yalnız Buyer öz sifarişlərini görə bilər
    [HttpGet("my")]
    [Authorize(Policy = OrderPermissions.Create)] // Buyer üçün
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = CurrentUserId();
        var orders = await _service.GetMyOrdersAsync(userId);
        return Ok(orders);
    }

    // Admin, Moderator, Seller bütün sifarişləri görə bilər
    [HttpGet]
    [Authorize(Policy = OrderPermissions.ViewAll)]
    public async Task<IActionResult> GetAll()
    {
        var role = CurrentUserRole();
        var orders = await _service.GetAllOrdersAsync(role);
        return Ok(orders);
    }

    // Yalnız Admin sifarişi update edə bilər
    [HttpPut("{id}")]
    [Authorize(Policy = OrderPermissions.Update)]
    public async Task<IActionResult> Update(Guid id, [FromBody] OrderUpdateDto dto)
    {
        var order = await _service.UpdateOrderAsync(id, dto);
        return Ok(order);
    }

    // Yalnız Admin sifarişi silə bilər
    [HttpDelete("{id}")]
    [Authorize(Policy = OrderPermissions.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteOrderAsync(id);
        return NoContent();
    }
}
