using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Domain.Entities;

public class Order:BaseEntity
{
    //public Guid UserId { get; set; }
    //public AppUser User { get; set; }  // İstifadəçi əlaqəsi

    public decimal TotalAmount { get; set; }  // Sifarişin ümumi qiyməti
    public string Status { get; set; } = "Pending"; // Pending, Processing, Shipped, Delivered, Cancelled
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    // Ünvan məlumatları (böyük layihədə lazım ola bilər)
    public string ShippingAddress { get; set; }
    public string BillingAddress { get; set; }

    // Ödəniş növü
    public string PaymentMethod { get; set; } = "CashOnDelivery"; // Card, PayPal, Stripe və s.
    public bool IsPaid { get; set; } = false;

    // Navigasiyalar
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
