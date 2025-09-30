using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.OrderDTOs;

public class OrderResponseDto
{
    public Guid Id { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public string ShippingAddress { get; set; } = null!;
    public string BillingAddress { get; set; } = null!;
    public string PaymentMethod { get; set; } = null!;
    public bool IsPaid { get; set; }
}
