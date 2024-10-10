using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Handlers;

public class AddBasketItemCommandHandler : IRequestHandler<AddBasketItemCommandRequest, AddBasketItemCommandResponse>
{
    private readonly IBasketItemService _basketItemService;

    public AddBasketItemCommandHandler(IBasketItemService basketItemService)
    {
        _basketItemService = basketItemService;
    }

    async Task<AddBasketItemCommandResponse> IRequestHandler<AddBasketItemCommandRequest, AddBasketItemCommandResponse>.Handle(AddBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketItemService.AddBasketItemAsync(new() { ProductId = request.ProductId, Quantity = request.Quantity});
        return new AddBasketItemCommandResponse();
    }
}
