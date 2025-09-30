using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Domain.Entities;

public class Cart:BaseEntity
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; }  // Gələcəkdə AppUser əlavə ediləcək

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    // Yeni xüsusiyyətlər
    public bool IsActive { get; set; } = true; // aktiv / passiv cart
    public decimal TotalAmount { get; set; } = 0; // toplam qiymət (optional, performance üçün)
}
