using AutoMapper;
using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.ProductImageDTOs;
using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class ProductImageService : IProductImageService
{
    private readonly IProductImageRepository _repository;
    private readonly IMapper _mapper;

    public ProductImageService(IProductImageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductImageResponseDto> CreateAsync(ProductImageCreateDto dto)
    {
        var entity = _mapper.Map<ProductImage>(dto);
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        return _mapper.Map<ProductImageResponseDto>(entity);
    }

    public async Task<ProductImageResponseDto> UpdateAsync(Guid id, ProductImageUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new KeyNotFoundException("ProductImage not found");

        _mapper.Map(dto, entity);
        _repository.Update(entity);
        await _repository.SaveChangesAsync();
        return _mapper.Map<ProductImageResponseDto>(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new KeyNotFoundException("ProductImage not found");

        _repository.Remove(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<ProductImageResponseDto>> GetByProductIdAsync(Guid productId)
    {
        var list = await _repository.GetByProductIdAsync(productId);
        return _mapper.Map<List<ProductImageResponseDto>>(list);
    }
}
