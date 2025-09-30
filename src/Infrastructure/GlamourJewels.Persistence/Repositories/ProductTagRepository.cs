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

public class ProductTagRepository : IProductTagRepository
{
    private readonly GlamourJewelsDbContext _context;

    public ProductTagRepository(GlamourJewelsDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ProductTag entity)
    {
        await _context.Set<ProductTag>().AddAsync(entity);
    }

    public void Update(ProductTag entity)
    {
        _context.Set<ProductTag>().Update(entity);
    }

    public void Remove(ProductTag entity)
    {
        _context.Set<ProductTag>().Remove(entity);
    }

    public async Task<ProductTag> GetByIdAsync(Guid id)
    {
        return await _context.Set<ProductTag>().FindAsync(id);
    }

    public async Task<IEnumerable<ProductTag>> GetAllAsync()
    {
        return await _context.Set<ProductTag>().ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProductTag>> GetByProductIdAsync(Guid productId)
    {
        return await _context.ProductTags
            .Where(pt => pt.ProductId == productId)
            .ToListAsync();
    }
}
