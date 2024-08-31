using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Users.GoogleLogInUsers;

public class GoogleLogInUserCommandRequestDto
{    
    public string Id { get; set; }
    public string IdToken { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhotoUrl { get; set; }
    public string Provider { get; set; }
    public int Second { get; set; } = 60;
}
