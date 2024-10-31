using MediatR;
using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;
using ResultHandler.Interfaces.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Commands.Create;

public class CreateOrderCommandRequest : IRequest<IResult>
{
   // public string Address { get; set; } = default!;
   // public string Description { get; set; } = default!;
    public CreateOrderDto CreateOrderDto { get; set; }

    public CreateOrderCommandRequest(CreateOrderDto createOrderDto)
    {
        CreateOrderDto = createOrderDto;
    }
}
