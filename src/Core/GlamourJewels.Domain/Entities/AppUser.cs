using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Domain.Entities;
public class AppUser:IdentityUser
{
    public string Fullname { get; set; } = null!;
    public string? RefreshToken { get; set; } = null!;
    public DateTime? ExpiryDate { get; set; }
}
