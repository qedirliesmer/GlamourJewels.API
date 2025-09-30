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

public class OrderItemRepository:IOrderItemRepository
{
    private readonly GlamourJewelsDbContext _context;
    private readonly DbSet<OrderItem> _dbSet;

    public OrderItemRepository(GlamourJewelsDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<OrderItem>();
    }

    public async Task<OrderItem> GetByIdAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException($"OrderItem with id {id} not found.");
        return entity;
    }

    public async Task<IEnumerable<OrderItem>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task AddAsync(OrderItem entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
    }

    public void Update(OrderItem entity) => _dbSet.Update(entity);

    public void Remove(OrderItem entity) => _dbSet.Remove(entity);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task<List<OrderItem>> GetByOrderIdAsync(Guid orderId) =>
        await _dbSet.Where(x => x.OrderId == orderId).ToListAsync();
}
