using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.OrderItemDTOs;

public class OrderItemUpdateDto
{
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
    public decimal? Discount { get; set; }
    public string? ProductVariant { get; set; }
    public string? Notes { get; set; }
}
