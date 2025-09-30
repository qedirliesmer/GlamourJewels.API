using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Repositories;

public interface IReviewRepository : IRepository<Review>
{
    Task<List<Review>> GetByProductIdAsync(Guid productId);
    Task<List<Review>> GetByUserIdAsync(string userId);
}