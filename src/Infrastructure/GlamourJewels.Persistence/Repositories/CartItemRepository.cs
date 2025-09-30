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

public class CartItemRepository : ICartItemRepository
{
    private readonly GlamourJewelsDbContext _context;
    private readonly DbSet<CartItem> _dbSet;

    public CartItemRepository(GlamourJewelsDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<CartItem>();
    }

    public async Task<CartItem> GetByIdAsync(Guid id)
    {
        var entity = await _dbSet.Include(ci => ci.Cart).FirstOrDefaultAsync(ci => ci.Id == id);
        if (entity == null) throw new KeyNotFoundException($"CartItem with id {id} not found.");
        return entity;
    }

    public async Task<IEnumerable<CartItem>> GetAllAsync()
        => await _dbSet.Include(ci => ci.Cart).ToListAsync();

    public async Task AddAsync(CartItem entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
    }

    public void Update(CartItem entity) => _dbSet.Update(entity);

    public void Remove(CartItem entity) => _dbSet.Remove(entity);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task<List<CartItem>> GetByCartIdAsync(Guid cartId)
        => await _dbSet.Where(ci => ci.CartId == cartId).ToListAsync();

    public async Task<CartItem?> GetByCartAndProductAsync(Guid cartId, Guid productId)
        => await _dbSet.FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
}