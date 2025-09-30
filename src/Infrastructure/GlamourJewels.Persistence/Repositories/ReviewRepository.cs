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

public class ReviewRepository : IReviewRepository
{
    private readonly GlamourJewelsDbContext _context;
    private readonly DbSet<Review> _dbSet;

    public ReviewRepository(GlamourJewelsDbContext context)
    {
        _context = context;
        _dbSet = context.Set<Review>();
    }

    public async Task<Review> GetByIdAsync(Guid id)
    {
        var r = await _dbSet.Include(x => x.Product)
                            .Include(x => x.User)
                            .FirstOrDefaultAsync(x => x.Id == id);
        if (r == null) throw new KeyNotFoundException($"Review with id {id} not found.");
        return r;
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
        => await _dbSet.Include(x => x.Product).Include(x => x.User).ToListAsync();

    public async Task AddAsync(Review entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
    }

    public void Update(Review entity) => _dbSet.Update(entity);

    public void Remove(Review entity) => _dbSet.Remove(entity);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task<List<Review>> GetByProductIdAsync(Guid productId)
        => await _dbSet.Where(r => r.ProductId == productId).Include(r => r.User).ToListAsync();

    public async Task<List<Review>> GetByUserIdAsync(string userId)
        => await _dbSet.Where(r => r.UserId == userId).Include(r => r.Product).ToListAsync();
}