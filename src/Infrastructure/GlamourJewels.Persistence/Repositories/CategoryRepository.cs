using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Domain.Entities;
using GlamourJewels.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Repositories;

public class CategoryRepository:ICategoryRepository
{
    private readonly GlamourJewelsDbContext _context;

    public CategoryRepository(GlamourJewelsDbContext context)
    {
        _context = context;
    }

    // IRepository implementasiyası
    public async Task<Category> GetByIdAsync(Guid id)
    {
        return await _context.Categories
            .Include(c => c.SubCategories) // nested categories
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories
            .Include(c => c.SubCategories)
            .ToListAsync();
    }

    public async Task AddAsync(Category entity)
    {
        await _context.Categories.AddAsync(entity);
        await SaveChangesAsync();
    }

    public void Update(Category entity)
    {
        _context.Categories.Update(entity);
    }

    public void Remove(Category entity)
    {
        _context.Categories.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    // İxtisaslaşdırılmış metodlar
    public async Task<Category> GetWithSubCategoriesAsync(Guid id)
    {
        return await _context.Categories
            .Include(c => c.SubCategories)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Category>> GetRootCategoriesAsync()
    {
        return await _context.Categories
            .Include(c => c.SubCategories)
            .Where(c => c.ParentId == null)
            .ToListAsync();
    }
}
