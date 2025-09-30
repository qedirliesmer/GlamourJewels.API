using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.OrderDTOs;

public class OrderCreateDto
{
    public string ShippingAddress { get; set; } = null!;
    public string BillingAddress { get; set; } = null!;
    public string PaymentMethod { get; set; } = "CashOnDelivery";
}
