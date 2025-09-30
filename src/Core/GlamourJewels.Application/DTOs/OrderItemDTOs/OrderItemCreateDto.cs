using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.OrderItemDTOs;

public class OrderItemCreateDto
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal Price { get; set; }
    public decimal Discount { get; set; } = 0;
    public string? ProductVariant { get; set; }
    public string? Notes { get; set; }
}
