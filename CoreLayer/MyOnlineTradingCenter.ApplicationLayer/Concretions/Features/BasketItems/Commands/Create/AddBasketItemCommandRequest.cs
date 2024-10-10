using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Commands.Create;

public class AddBasketItemCommandRequest : IRequest<AddBasketItemCommandResponse>
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}
