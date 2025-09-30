using AutoMapper;
using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Application.Abstracts.Services;
using GlamourJewels.Application.DTOs.CategoryDTOs;
using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Services;

public class CategoryService:ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CategoryResponseDto> GetByIdAsync(Guid id)
    {
        var category = await _repository.GetWithSubCategoriesAsync(id);
        if (category == null)
            throw new KeyNotFoundException("Category tapılmadı");

        return _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _repository.GetRootCategoriesAsync();
        return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
    }

    public async Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        await _repository.AddAsync(category);
        return _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<CategoryResponseDto> UpdateAsync(Guid id, CategoryUpdateDto dto)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            throw new KeyNotFoundException("Category tapılmadı");

        _mapper.Map(dto, category);
        _repository.Update(category);
        await _repository.SaveChangesAsync();

        return _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            throw new KeyNotFoundException("Category tapılmadı");

        _repository.Remove(category);
        await _repository.SaveChangesAsync();
    }
}
