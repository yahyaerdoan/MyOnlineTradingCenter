using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;

public class CreateOrderDto
{
    public string UserId { get; set; }
    public string OrderNumber { get; set; }  
    public string Address { get; set; }
    public string Description { get; set; }

}
