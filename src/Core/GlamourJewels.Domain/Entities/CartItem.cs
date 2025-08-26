using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Domain.Entities;

public class CartItem:BaseEntity
{
    public Guid CartId { get; set; }
    public Cart Cart { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    // Məhsul sayı
    public int Quantity { get; set; }

    // Yeni xüsusiyyətlər
    public decimal Price { get; set; } // məhsulun o anki qiyməti (discount tətbiqi ilə)
    public bool IsSelected { get; set; } = true; // Checkout üçün seçilib / seçilməyib
}
