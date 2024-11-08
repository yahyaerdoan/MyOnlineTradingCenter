using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.ViewModels.BasketItems;

public class CreateBasketItemViewModel
{
    public string ProductId { get; set; } = default!;
    public int Quantity { get; set; }
}
