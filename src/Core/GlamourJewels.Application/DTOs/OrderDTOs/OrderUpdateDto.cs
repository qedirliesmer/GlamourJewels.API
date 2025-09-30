using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.OrderDTOs;

public class OrderUpdateDto
{
    public string? Status { get; set; } 
    public bool? IsPaid { get; set; }
    public string? ShippingAddress { get; set; }
    public string? BillingAddress { get; set; }
    public string? PaymentMethod { get; set; }
}
