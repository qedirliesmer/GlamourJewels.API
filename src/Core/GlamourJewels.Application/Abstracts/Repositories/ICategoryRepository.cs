using GlamourJewels.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Abstracts.Repositories;

public interface ICategoryRepository:IRepository<Category>
{
    Task<Category> GetWithSubCategoriesAsync(Guid id);
    Task<IEnumerable<Category>> GetRootCategoriesAsync(); // ParentId == null olanlar
}
