using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users;

public class CreateUserCommandResponseDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
