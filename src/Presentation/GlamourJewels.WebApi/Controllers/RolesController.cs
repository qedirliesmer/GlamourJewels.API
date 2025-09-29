using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.RoleDTOs;
using GlamourJewels.Application.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlamourJewels.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private IRoleService _roleService { get; }
    public RolesController( IRoleService roleService)
    {
        _roleService = roleService;
    }
    // GET: api/<RolesController>
    [HttpGet("permissions")]
    public IActionResult GetAllPermissions()
    {
        var permissions = PermissionHelper.GetAllPermissions();
        return Ok(permissions);
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoleCreateDto dto)
    {
        var result = await _roleService.CreateRole(dto);
        return StatusCode((int)result.StatusCode,result);
    }

}
