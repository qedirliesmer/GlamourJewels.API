﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.DTOs.UserDTOs;

public class UserAddRoleDto
{
    public Guid UserId { get; set; }
    public List<Guid> RolesId { get; set; }
}
