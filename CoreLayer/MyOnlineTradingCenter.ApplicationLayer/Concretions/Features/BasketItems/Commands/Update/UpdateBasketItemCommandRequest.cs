using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Commands.Update;

public class UpdateBasketItemCommandRequest : IRequest<UpdateBasketItemCommandResponse>
{
    public string BasketItemId { get; set; }
    public int Quantity { get; set; }
}
