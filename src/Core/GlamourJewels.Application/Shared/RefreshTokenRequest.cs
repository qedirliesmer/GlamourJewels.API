﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamourJewels.Application.Shared;

public class RefreshTokenRequest
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}
