using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Repositories;

public interface IProductRepository:IRepository<Product>
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product?> GetProductWithDetailsAsync(Guid id);
    Task AddAsync(Product entity);
    void Update(Product entity);
    void Remove(Product entity);
    Task SaveChangesAsync();
}
