using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.UserDTOs;
using GlamourJewels.Application.Shared;
using GlamourJewels.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class UserService : IUserService
{
    private UserManager<AppUser> _userManager { get; }
    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<BaseResponse<string>> Register(UserRegisterDto dto)
    {
        var existedEmail=await _userManager.FindByEmailAsync(dto.Email);
        if (existedEmail is not null)
        {
            return new BaseResponse<string>("This account already exist", System.Net.HttpStatusCode.BadRequest);
        }

        AppUser newUser = new()
        {
            Email = dto.Email,
            Fullname=dto.Fullname,
            UserName = dto.Email
        };

        IdentityResult identityResult=await _userManager.CreateAsync(newUser,dto.Password);
        if (!identityResult.Succeeded)
        {
            var errors=identityResult.Errors;
            StringBuilder errorsMessage = new();
            foreach (var error in errors)
            {
                errorsMessage.Append(error.Description + "\n");
            }
            return new(errorsMessage.ToString(),HttpStatusCode.BadRequest);
        }
        return new ("Successfully created", HttpStatusCode.Created);
    }
}
