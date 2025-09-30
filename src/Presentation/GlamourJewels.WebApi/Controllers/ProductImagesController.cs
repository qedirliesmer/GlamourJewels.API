using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.ProductImageDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GlamourJewels.Application.Shared.Permissions;

namespace GlamourJewels.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductImagesController : ControllerBase
{
    private readonly IProductImageService _service;

    public ProductImagesController(IProductImageService service)
    {
        _service = service;
    }

    [HttpGet("{productId}")]
    [Authorize(Policy = ProductImagePermissions.View)]
    public async Task<IActionResult> GetByProductId(Guid productId)
    {
        var list = await _service.GetByProductIdAsync(productId);
        return Ok(list);
    }

    [HttpPost]
    [Authorize(Policy = ProductImagePermissions.Create)]
    public async Task<IActionResult> Create(ProductImageCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = ProductImagePermissions.Update)]
    public async Task<IActionResult> Update(Guid id, ProductImageUpdateDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = ProductImagePermissions.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
