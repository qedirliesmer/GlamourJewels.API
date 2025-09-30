using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.ReviewDTOs;

public class ReviewCreateDto
{
    public Guid ProductId { get; set; }
    public int Rating { get; set; } // 1-5
    public string Comment { get; set; } = null!;
    public bool IsAnonymous { get; set; } = false;
}