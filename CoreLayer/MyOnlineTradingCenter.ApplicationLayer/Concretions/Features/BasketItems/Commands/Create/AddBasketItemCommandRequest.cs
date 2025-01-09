using MediatR;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Commands.Create;

public class AddBasketItemCommandRequest : IRequest<AddBasketItemCommandResponse>
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}
