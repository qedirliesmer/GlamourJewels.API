using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Repositories;

public interface IFavoriteRepository : IRepository<Favorite>
{
    Task<List<Favorite>> GetByUserIdAsync(Guid userId);
    Task<Favorite?> GetByUserAndProductAsync(Guid userId, Guid productId);
}