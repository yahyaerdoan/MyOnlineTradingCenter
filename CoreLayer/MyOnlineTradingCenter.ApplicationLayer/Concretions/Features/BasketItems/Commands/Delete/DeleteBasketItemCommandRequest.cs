using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Commands.Delete;

public class DeleteBasketItemCommandRequest : IRequest<DeleteBasketItemCommandResponse>
{
    public string BasketItemId { get; set; }
}
