using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Commands.Delete;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Handlers;

public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommandRequest, DeleteBasketItemCommandResponse>
{
    private readonly IBasketItemService _basketItemService;

    public DeleteBasketItemCommandHandler(IBasketItemService basketItemService)
    {
        _basketItemService = basketItemService;
    }

    public async Task<DeleteBasketItemCommandResponse> Handle(DeleteBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketItemService.RemoveBasketItemAsync(request.BasketItemId);
        return new DeleteBasketItemCommandResponse();
    }
}
