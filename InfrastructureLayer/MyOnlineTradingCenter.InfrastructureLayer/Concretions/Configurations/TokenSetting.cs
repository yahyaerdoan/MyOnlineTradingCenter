using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.Configurations;

public class TokenSetting
{
    public int AccessTokenLifeTime { get; set; }
    public int RefreshTokenLifeTime { get; set; }
}
