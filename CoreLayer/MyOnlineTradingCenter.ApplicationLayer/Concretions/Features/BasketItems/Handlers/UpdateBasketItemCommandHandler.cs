using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Commands.Update;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Handlers;

public class UpdateBasketItemCommandHandler : IRequestHandler<UpdateBasketItemCommandRequest, UpdateBasketItemCommandResponse>
{
    private readonly IBasketItemService _basketItemService;

    public UpdateBasketItemCommandHandler(IBasketItemService basketItemService)
    {
        _basketItemService = basketItemService;
    }

    public async Task<UpdateBasketItemCommandResponse> Handle(UpdateBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketItemService.UpdateBasketItemQuantityAsync(new() { BasketItemId = request.BasketItemId, Quantity = request.Quantity });
        return new UpdateBasketItemCommandResponse();
    }
}
