using GlamourJewels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Persistence.Contexts;

public class GlamourJewelsDbContext:DbContext
{
    public GlamourJewelsDbContext(DbContextOptions<GlamourJewelsDbContext> options):base(options)
    {  
    }

    public DbSet<Category> Categories { get; set; }
}
