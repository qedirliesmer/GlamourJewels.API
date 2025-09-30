using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Repositories;

public interface ICartItemRepository : IRepository<CartItem>
{
    Task<List<CartItem>> GetByCartIdAsync(Guid cartId);
    Task<CartItem?> GetByCartAndProductAsync(Guid cartId, Guid productId);
}