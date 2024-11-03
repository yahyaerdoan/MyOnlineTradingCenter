using MediatR;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Commands.Update;

public class UpdateBasketItemCommandRequest : IRequest<UpdateBasketItemCommandResponse>
{
    public string BasketItemId { get; set; } = default!;
    public int Quantity { get; set; }
}
