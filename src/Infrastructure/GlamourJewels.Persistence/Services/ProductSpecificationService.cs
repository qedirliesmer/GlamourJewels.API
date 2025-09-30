using AutoMapper;
using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.ProductSpecificationDTOs;
using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class ProductSpecificationService : IProductSpecificationService
{
    private readonly IProductSpecificationRepository _repository;
    private readonly IMapper _mapper;

    public ProductSpecificationService(IProductSpecificationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductSpecificationResponseDto> CreateAsync(ProductSpecificationCreateDto dto)
    {
        var entity = _mapper.Map<ProductSpecification>(dto);
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        return _mapper.Map<ProductSpecificationResponseDto>(entity);
    }

    public async Task<ProductSpecificationResponseDto> UpdateAsync(Guid id, ProductSpecificationUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Specification not found");

        _mapper.Map(dto, entity);
        _repository.Update(entity);
        await _repository.SaveChangesAsync();

        return _mapper.Map<ProductSpecificationResponseDto>(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Specification not found");

        _repository.Remove(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task<ProductSpecificationResponseDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Specification not found");
        return _mapper.Map<ProductSpecificationResponseDto>(entity);
    }

    public async Task<List<ProductSpecificationResponseDto>> GetByProductIdAsync(Guid productId)
    {
        var list = await _repository.GetByProductIdAsync(productId);
        return _mapper.Map<List<ProductSpecificationResponseDto>>(list);
    }
}