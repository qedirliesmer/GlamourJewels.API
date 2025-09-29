using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.UserDTOs;
using GlamourJewels.Application.Shared;
using GlamourJewels.Application.Shared.Settings;
using GlamourJewels.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class UserService : IUserService
{
    private UserManager<AppUser> _userManager { get; }
    private RoleManager<IdentityRole> _roleManager { get; }
    private SignInManager<AppUser> _signInManager { get; }
    public JWTSettings _jwtSetting { get; }
    public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<JWTSettings> jwtSetting, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSetting = jwtSetting.Value;
        _roleManager = roleManager;
    }

    public async Task<BaseResponse<string>> Register(UserRegisterDto dto)
    {
        var existedEmail = await _userManager.FindByEmailAsync(dto.Email);
        if (existedEmail is not null)
        {
            return new BaseResponse<string>("This account already exist", System.Net.HttpStatusCode.BadRequest);
        }

        AppUser newUser = new()
        {
            Email = dto.Email,
            Fullname = dto.Fullname,
            UserName = dto.Email
        };

        IdentityResult identityResult = await _userManager.CreateAsync(newUser, dto.Password);
        if (!identityResult.Succeeded)
        {
            var errors = identityResult.Errors;
            StringBuilder errorsMessage = new();
            foreach (var error in errors)
            {
                errorsMessage.Append(error.Description + ";");
            }
            return new(errorsMessage.ToString(), HttpStatusCode.BadRequest);
        }
        return new("Successfully created", HttpStatusCode.Created);
    }

    public async Task<BaseResponse<TokenResponse>> Login(UserLoginDto dto)
    {
        var existedUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existedUser is null)
        {
            return new("Email or password is wrong.", null, HttpStatusCode.NotFound);
        }
        SignInResult signInResult = await _signInManager.PasswordSignInAsync
            (dto.Email, dto.Password, true, true);
        if (!signInResult.Succeeded)
        {
            return new("Email or password is wrong.", null, HttpStatusCode.NotFound);
        }
        var token =await GenerateTokenAsync(existedUser);
        return new("Token generated", token, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var principal = GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null)
            return new("Invalid access token", null, HttpStatusCode.BadRequest);

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByIdAsync(userId!);

        if (user == null )
        {
            return new("User not found", null, HttpStatusCode.NotFound);
        }

        if (user.RefreshToken is null || user.RefreshToken!=request.RefreshToken || user.ExpiryDate<DateTime.UtcNow)
        {
            return new ("Invalid refresh token",  null, HttpStatusCode.BadRequest);
        }
   
        var tokenResponse = await GenerateTokenAsync(user);
        return new("Token refreshed", tokenResponse, HttpStatusCode.OK);
    }

    //public async Task<BaseResponse<string>> AddRole(UserAddRoleDto dto)
    //{
    //    var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
    //    if (user == null)
    //    {
    //       return new BaseResponse<string>("User not found", HttpStatusCode.NotFound);
    //    }

    //    var roleNames = new List<string>(); 

    //    foreach (var roleId in dto.RolesId.Distinct())
    //    {

    //            var role = await _roleManager.FindByIdAsync(roleId.ToString());
    //            if (role==null)
    //            {
    //                return new BaseResponse<string>($"Role with ID '{roleId}' not found",
    //                    HttpStatusCode.NotFound);
    //            }

    //            if(!await _userManager.IsInRoleAsync(user, role.Name!))
    //            {
    //                var result=await _userManager.AddToRoleAsync(user,role.Name!);
    //                if (!result.Succeeded)
    //                {
    //                    var errors=string.Join(";", result.Errors.Select(e=>e.Description));
    //                    return new BaseResponse<string>($"Failed to add role '{role.Name}' to user:{errors}", HttpStatusCode.BadRequest);
    //                }
    //                roleNames.Add(role.Name!);
    //            }    
    //    }

    //    return new BaseResponse<string>($"Successfully added roles: {string.Join(",", roleNames)} to user", HttpStatusCode.OK);


    //}
    public async Task<BaseResponse<string>> AddRole(UserAddRoleDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
        if (user == null)
        {
            return new BaseResponse<string>("User not found", HttpStatusCode.NotFound);
        }

        var roleNames = new List<string>();

        foreach (var roleId in dto.RolesId.Distinct())
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                return new BaseResponse<string>(
                    $"Role with ID '{roleId}' not found",
                    HttpStatusCode.NotFound
                );
            }

            if (string.IsNullOrEmpty(role.Name))
            {
                return new BaseResponse<string>(
                    $"Role with ID '{roleId}' has no valid name",
                    HttpStatusCode.BadRequest
                );
            }

            if (!await _userManager.IsInRoleAsync(user, role.Name))
            {
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (!result.Succeeded)
                {
                    var errors = string.Join(";", result.Errors.Select(e => e.Description));
                    return new BaseResponse<string>(
                        $"Failed to add role '{role.Name}' to user: {errors}",
                        HttpStatusCode.BadRequest
                    );
                }
                roleNames.Add(role.Name);
            }
        }

        return new BaseResponse<string>(
            $"Successfully added roles: {string.Join(",", roleNames)} to user",
            HttpStatusCode.OK
        );
    }

    private async Task<TokenResponse> GenerateTokenAsync(AppUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSetting.SecretKey);

        var claims = new List<Claim>
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email!)
        };

        var roles= await _userManager.GetRolesAsync(user);
        foreach (var roleName in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, roleName));

            var role=await _roleManager.FindByNameAsync(roleName);
            if(role != null)
            {
                var roleClaims =await _roleManager.GetClaimsAsync(role);
                var permissionClaims = roleClaims
                    .Where(c => c.Type == "Permission")
                    .Distinct();

                foreach (var permissionClaim in permissionClaims)
                {
                    claims.Add(new Claim("Permission", permissionClaim.Value));
                }
            }
        }
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtSetting.Issuer,
            Audience = _jwtSetting.Audience,
            NotBefore = DateTime.UtcNow, // token indidən etibarən etibarlı olsun
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(_jwtSetting.ExpiryMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiryDate = DateTime.UtcNow.AddHours(2);
        user.RefreshToken = refreshToken;
        user.ExpiryDate = refreshTokenExpiryDate;
        await _userManager.UpdateAsync(user);

        return new TokenResponse
        {
            Token = jwt,
            RefreshToken = refreshToken,
            ExpiryDate = tokenDescriptor.Expires!.Value
        };

    }
    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true, 
            ValidateIssuer = true, 
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            ValidIssuer=_jwtSetting.Issuer,
            ValidAudience=_jwtSetting.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey)),
            
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is JwtSecurityToken jwtSecurityToken &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }
        }
        catch
        {
            return null;
        }

        return null;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

}


