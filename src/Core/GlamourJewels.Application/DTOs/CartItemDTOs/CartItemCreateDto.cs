using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.CartItemDTOs;

public class CartItemCreateDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal Price { get; set; } // göndərilsə istifadə olunar; əks halda product-un qiyməti istifadə oluna bilər (project-dependent)
    public string? ProductVariant { get; set; }
    public string? Notes { get; set; }
}
