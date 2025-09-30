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

public class ProductSpecificationRepository : IProductSpecificationRepository
{
    private readonly GlamourJewelsDbContext _context;
    private readonly DbSet<ProductSpecification> _dbSet;

    public ProductSpecificationRepository(GlamourJewelsDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<ProductSpecification>();
    }

    public async Task<ProductSpecification> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<ProductSpecification>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(ProductSpecification entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(ProductSpecification entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(ProductSpecification entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProductSpecification>> GetByProductIdAsync(Guid productId)
    {
        return await _dbSet
            .Where(ps => ps.ProductId == productId)
            .ToListAsync();
    }
}