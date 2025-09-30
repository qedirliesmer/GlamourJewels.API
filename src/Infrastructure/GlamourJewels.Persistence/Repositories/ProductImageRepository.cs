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

public class ProductImageRepository : IProductImageRepository
{
    private readonly GlamourJewelsDbContext _context;
    private readonly DbSet<ProductImage> _dbSet;

    public ProductImageRepository(GlamourJewelsDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<ProductImage>();
    }

    public async Task AddAsync(ProductImage entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<IEnumerable<ProductImage>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<ProductImage> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException("ProductImage not found");
    }

    public void Remove(ProductImage entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(ProductImage entity)
    {
        _dbSet.Update(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProductImage>> GetByProductIdAsync(Guid productId)
    {
        return await _dbSet.Where(pi => pi.ProductId == productId).ToListAsync();
    }
}
