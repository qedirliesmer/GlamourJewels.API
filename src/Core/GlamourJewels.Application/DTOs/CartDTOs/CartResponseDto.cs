using GlamourJewels.Application.DTOs.CartItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.CartDTOs;

public class CartResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public bool IsActive { get; set; }
    public decimal TotalAmount { get; set; }
    public List<CartItemResponseDto> CartItems { get; set; } = new();
}
