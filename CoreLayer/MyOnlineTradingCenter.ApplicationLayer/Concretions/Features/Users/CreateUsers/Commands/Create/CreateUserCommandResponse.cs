﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.CreateUsers.Commands.Create;

public class CreateUserCommandResponse
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
