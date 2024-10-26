using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.BasketItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Baskets;

public class BasketDto
{
    public string UserId { get; set; } = default!;
    public List<BasketItemDto> Items { get; set; }

    public BasketDto() => Items = new List<BasketItemDto>();
}
