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

public class OrderRepository:IOrderRepository
{
    private readonly GlamourJewelsDbContext _context;
    private readonly DbSet<Order> _dbSet;

    public OrderRepository(GlamourJewelsDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Order>();
    }

    // IRepository metodları
    public async Task<Order> GetByIdAsync(Guid id)
    {
        var order = await _dbSet.FindAsync(id);
        if (order == null)
            throw new KeyNotFoundException($"Order with id {id} not found.");
        return order;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(Order entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
    }

    public void Update(Order entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(Order entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    // Order xüsusi metodu
    public async Task<List<Order>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }
}
