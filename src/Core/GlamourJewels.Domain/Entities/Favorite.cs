using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Domain.Entities;

public class Favorite:BaseEntity
{
    //public Guid UserId { get; set; }
    //public AppUser User { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    //public Guid UserId { get; set; }
    //public AppUser User { get; set; } // İstifadəçi əlaqəsi

    // Yeni xüsusiyyətlər
    public bool IsActive { get; set; } = true; // favorit silinibsə passiv olur
    public DateTime FavoritedAt { get; set; } = DateTime.UtcNow; // favoritə əlavə edilmə tarixi
}
