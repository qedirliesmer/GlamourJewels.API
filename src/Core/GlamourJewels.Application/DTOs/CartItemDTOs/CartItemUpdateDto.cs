using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.CartItemDTOs;

public class CartItemUpdateDto
{

    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
    public string? ProductVariant { get; set; }
    public string? Notes { get; set; }
    public bool? IsSelected { get; set; }
}
