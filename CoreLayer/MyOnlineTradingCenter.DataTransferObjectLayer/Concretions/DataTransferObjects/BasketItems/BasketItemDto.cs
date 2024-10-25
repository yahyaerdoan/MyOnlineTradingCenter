using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.BasketItems;

public class BasketItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtTimeOfAddition { get; set; }
    public string ProductName { get; set; }
}
