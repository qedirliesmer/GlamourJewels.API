using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Repositories;

public interface IProductSpecificationRepository : IRepository<ProductSpecification>
{
    Task<List<ProductSpecification>> GetByProductIdAsync(Guid productId);
}