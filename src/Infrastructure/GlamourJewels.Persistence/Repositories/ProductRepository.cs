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

public class ProductRepository:IProductRepository
{
    private readonly GlamourJewelsDbContext _context;

    public ProductRepository(GlamourJewelsDbContext context)
    {
        _context = context;
    }

    // public async Task<Product?> GetByIdAsync(Guid id)
    //  => await _context.Products.FindAsync(id);
    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
                             .Include(p => p.Category)
                             .Include(p => p.Images)
                             .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _context.Products.Include(p => p.Category).Include(p => p.Images).ToListAsync();

    public async Task<Product?> GetProductWithDetailsAsync(Guid id)
        => await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Product entity)
        => await _context.Products.AddAsync(entity);

    public void Update(Product entity)
        => _context.Products.Update(entity);

    public void Remove(Product entity)
        => _context.Products.Remove(entity);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
