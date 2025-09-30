using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.CartDTOs;

public class CartUpdateDto
{
    public bool? IsActive { get; set; }
    public decimal? TotalAmount { get; set; }
}
