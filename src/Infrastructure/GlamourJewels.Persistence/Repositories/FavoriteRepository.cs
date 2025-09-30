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

public class FavoriteRepository : IFavoriteRepository
{
    private readonly GlamourJewelsDbContext _context;
    private readonly DbSet<Favorite> _dbSet;

    public FavoriteRepository(GlamourJewelsDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Favorite>();
    }

    public async Task<Favorite> GetByIdAsync(Guid id)
    {
        var f = await _dbSet.Include(fav => fav.Product)
                            .Include(fav => fav.User)
                            .FirstOrDefaultAsync(fav => fav.Id == id);
        if (f == null) throw new KeyNotFoundException($"Favorite with id {id} not found.");
        return f;
    }

    public async Task<IEnumerable<Favorite>> GetAllAsync()
        => await _dbSet.Include(fav => fav.Product).Include(fav => fav.User).ToListAsync();

    public async Task AddAsync(Favorite entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
    }

    public void Update(Favorite entity) => _dbSet.Update(entity);

    public void Remove(Favorite entity) => _dbSet.Remove(entity);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task<List<Favorite>> GetByUserIdAsync(Guid userId)
        => await _dbSet.Where(f => f.UserId == userId).Include(f => f.Product).ToListAsync();

    public async Task<Favorite?> GetByUserAndProductAsync(Guid userId, Guid productId)
        => await _dbSet.FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);
}