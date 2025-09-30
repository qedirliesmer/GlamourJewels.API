using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Domain.Entities;

public class Review:BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public string UserId { get; set; }
    public AppUser User { get; set; } // İstifadəçi əlaqəsi

    // Rəy məlumatları
    public int Rating { get; set; } // 1-5
    public string Comment { get; set; }

    // Yeni xüsusiyyətlər
    public bool IsApproved { get; set; } = false; // Admin təsdiqi
    public bool IsAnonymous { get; set; } = false; // İstifadəçi adı gizli
    public DateTime? ApprovedAt { get; set; } // Admin təsdiq tarixi
}
