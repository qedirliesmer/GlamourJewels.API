using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.RoleDTOs;

public class RoleCreateDto
{
    public string Name { get; set; } = null!;
    public List<string> PermissionList { get; set; }
}
