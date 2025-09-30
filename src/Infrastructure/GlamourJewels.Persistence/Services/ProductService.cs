using AutoMapper;
using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.ProductDTOs;
using GlamourJewels.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class ProductService:IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public ProductService(IProductRepository productRepository, IMapper mapper, UserManager<AppUser> userManager)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetProductWithDetailsAsync(id);
        return product == null ? null : _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<ProductResponseDto> CreateAsync(ProductCreateDto dto, string userId)
    {
        var product = _mapper.Map<Product>(dto);
        product.AppUserId = userId; // kim yaratdı
        product.Slug = dto.Name.ToLower().Replace(" ", "-");

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<ProductResponseDto?> UpdateAsync(Guid id, ProductUpdateDto dto, string userId)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return null;

        // yalnız Admin və ya məhsul sahibi
        if (product.AppUserId != userId && !await UserIsInRole(userId, "Admin"))
            throw new UnauthorizedAccessException("Sənin bu məhsulu redaktə etməyə icazən yoxdur.");

        _mapper.Map(dto, product);
        product.Slug = dto.Name.ToLower().Replace(" ", "-");

        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync();

        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<bool> DeleteAsync(Guid id, string userId)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return false;

        // yalnız Admin və ya məhsul sahibi
        if (product.AppUserId != userId && !await UserIsInRole(userId, "Admin"))
            throw new UnauthorizedAccessException("Sənin bu məhsulu silməyə icazən yoxdur.");

        _productRepository.Remove(product);
        await _productRepository.SaveChangesAsync();
        return true;
    }

    // User rolunu yoxlamaq üçün helper
    private async Task<bool> UserIsInRole(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;
        return await _userManager.IsInRoleAsync(user, role);
    }
}
