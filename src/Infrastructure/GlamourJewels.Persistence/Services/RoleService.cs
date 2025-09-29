using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.RoleDTOs;
using GlamourJewels.Application.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class RoleService : IRoleService
{
    private RoleManager<IdentityRole> _roleManager { get; }
    public RoleService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task<BaseResponse<string?>> CreateRole(RoleCreateDto dto)
    {
        var existingRole=await _roleManager.FindByNameAsync(dto.Name);
        if (existingRole is not null)
            return new BaseResponse<string?>("Role already exist.", HttpStatusCode.BadRequest);

        // 2. Yeni rol yarat
        var identityRole = new IdentityRole(dto.Name);
        var result = await _roleManager.CreateAsync(identityRole);

        if (!result.Succeeded)
        {
            var errorMessages=string.Join(";", result.Errors.Select(e => e.Description));
            return new BaseResponse<string?>(errorMessages, HttpStatusCode.BadRequest);
        }

        foreach (var permission in dto.PermissionList.Distinct())
        {
            var claimResult = await _roleManager.AddClaimAsync(identityRole, new Claim("Permission", permission));
            if (!claimResult.Succeeded)
            {
                var error = string.Join(";", result.Errors.Select(e => e.Description));
                return new BaseResponse<string?>($"Role created, but adding permission '{permission}' failded: {error}", HttpStatusCode.PartialContent);


            }

        }

        return new BaseResponse<string?>("Role created successfully", true, HttpStatusCode.Created);
    }
}
