using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.CategoryDTOs;
using GlamourJewels.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlamourJewels.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [Authorize(Policy = Permissions.Category.View)]
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [Authorize(Policy = Permissions.Category.View)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var category = await _service.GetByIdAsync(id);
            return Ok(category);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Category tapılmadı" });
        }
    }

    [Authorize(Policy = Permissions.Category.Create)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var category = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [Authorize(Policy = Permissions.Category.Update)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CategoryUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updatedCategory = await _service.UpdateAsync(id, dto);
            return Ok(updatedCategory);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Category tapılmadı" });
        }
    }

    [Authorize(Policy = Permissions.Category.Delete)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Category tapılmadı" });
        }
    }
}
