﻿using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices.IAuthentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;

public interface IAuthService : IInternalAuthentication, IExternalAuthentication
{
}
