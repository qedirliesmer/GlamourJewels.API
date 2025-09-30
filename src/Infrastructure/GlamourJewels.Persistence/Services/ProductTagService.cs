using AutoMapper;
using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.ProductTagDTOs;
using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class ProductTagService : IProductTagService
{
    private readonly IProductTagRepository _repository;
    private readonly IMapper _mapper;

    public ProductTagService(IProductTagRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductTagResponseDto> CreateAsync(Guid productId, ProductTagCreateDto dto)
    {
        var entity = _mapper.Map<ProductTag>(dto);
        entity.ProductId = productId;
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        return _mapper.Map<ProductTagResponseDto>(entity);
    }

    public async Task<ProductTagResponseDto> UpdateAsync(Guid id, ProductTagUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("ProductTag tapılmadı");

        _mapper.Map(dto, entity);
        _repository.Update(entity);
        await _repository.SaveChangesAsync();

        return _mapper.Map<ProductTagResponseDto>(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("ProductTag tapılmadı");

        _repository.Remove(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<ProductTagResponseDto>> GetAllByProductIdAsync(Guid productId)
    {
        var list = await _repository.GetByProductIdAsync(productId);
        return _mapper.Map<List<ProductTagResponseDto>>(list);
    }

    public async Task<ProductTagResponseDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("ProductTag tapılmadı");
        return _mapper.Map<ProductTagResponseDto>(entity);
    }
}