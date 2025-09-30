using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.ProductSpecificationDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GlamourJewels.Application.Shared.Permissions;

namespace GlamourJewels.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductSpecificationsController : ControllerBase
{
    private readonly IProductSpecificationService _service;

    public ProductSpecificationsController(IProductSpecificationService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    [Authorize(Policy = ProductSpecificationPermissions.View)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var spec = await _service.GetByIdAsync(id);
        return Ok(spec);
    }

    [HttpGet("product/{productId}")]
    [Authorize(Policy = ProductSpecificationPermissions.View)]
    public async Task<IActionResult> GetByProductId(Guid productId)
    {
        var specs = await _service.GetByProductIdAsync(productId);
        return Ok(specs);
    }

    [HttpPost]
    [Authorize(Policy = ProductSpecificationPermissions.Create)]
    public async Task<IActionResult> Create([FromBody] ProductSpecificationCreateDto dto)
    {
        var spec = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = spec.Id }, spec);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = ProductSpecificationPermissions.Update)]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductSpecificationUpdateDto dto)
    {
        var spec = await _service.UpdateAsync(id, dto);
        return Ok(spec);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = ProductSpecificationPermissions.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
