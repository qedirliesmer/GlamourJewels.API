using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.ReviewDTOs;

public class ReviewResponseDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string UserId { get; set; } = null!; // owner id
    public string? UserName { get; set; } // optional — if not anonymous and included via mapping
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    public bool IsApproved { get; set; }
    public bool IsAnonymous { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public DateTime CreatedAt { get; set; } // from BaseEntity if present or OrderDate style
}
