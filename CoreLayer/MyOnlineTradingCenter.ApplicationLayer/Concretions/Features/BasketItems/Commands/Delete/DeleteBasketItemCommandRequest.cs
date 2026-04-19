using MediatR;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Commands.Delete;

public class DeleteBasketItemCommandRequest : IRequest<DeleteBasketItemCommandResponse>
{
    public string BasketItemId { get; set; }
}
