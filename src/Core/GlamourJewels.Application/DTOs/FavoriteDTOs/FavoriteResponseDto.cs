using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.FavoriteDTOs;

public class FavoriteResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public bool IsActive { get; set; }
    public DateTime FavoritedAt { get; set; }
}
