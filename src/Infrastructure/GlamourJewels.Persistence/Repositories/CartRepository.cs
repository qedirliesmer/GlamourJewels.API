using GlamourJewels.Application.Abstracts.Repositories;
using GlamourJewels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Repositories;

public class CartRepository:ICartRepository
{
    private readonly DbContext _context;

    public CartRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<Cart> GetByIdAsync(Guid id)
        => await _context.Set<Cart>().Include(c => c.CartItems).FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Cart>> GetAllAsync()
        => await _context.Set<Cart>().Include(c => c.CartItems).ToListAsync();

    public async Task AddAsync(Cart entity) => await _context.Set<Cart>().AddAsync(entity);
    public void Update(Cart entity) => _context.Set<Cart>().Update(entity);
    public void Remove(Cart entity) => _context.Set<Cart>().Remove(entity);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task<Cart> GetByUserIdAsync(Guid userId)
        => await _context.Set<Cart>().Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
}
