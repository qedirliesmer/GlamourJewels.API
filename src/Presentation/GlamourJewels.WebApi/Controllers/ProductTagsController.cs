using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.ProductTagDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GlamourJewels.Application.Shared.Permissions;

namespace GlamourJewels.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductTagsController : ControllerBase
{
    private readonly IProductTagService _service;

    public ProductTagsController(IProductTagService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Policy = ProductTagPermissions.View)]
    public async Task<IActionResult> GetAll(Guid productId)
    {
        var tags = await _service.GetAllByProductIdAsync(productId);
        return Ok(tags);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = ProductTagPermissions.View)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tag = await _service.GetByIdAsync(id);
        return Ok(tag);
    }

    [HttpPost]
    [Authorize(Policy = ProductTagPermissions.Create)]
    public async Task<IActionResult> Create(Guid productId, [FromBody] ProductTagCreateDto dto)
    {
        var tag = await _service.CreateAsync(productId, dto);
        return Ok(tag);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = ProductTagPermissions.Update)]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductTagUpdateDto dto)
    {
        var tag = await _service.UpdateAsync(id, dto);
        return Ok(tag);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = ProductTagPermissions.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
