using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Queries.Get;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Handlers;

public class GetBasketItemsCommandHandler : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>>
{
    private readonly IBasketItemService _basketItemService;

    public GetBasketItemsCommandHandler(IBasketItemService basketItemService)
    {
        _basketItemService = basketItemService;
    }

    public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
    {
        List<BasketItem> basketItems = await _basketItemService.GetBasketItemsAsync();
        return basketItems.Select(bi => new GetBasketItemsQueryResponse { BasketItemId = bi.Id.ToString(), Name = bi.Product.Name, Price = bi.Product.Price, Quantity = bi.Quantity }).ToList();
    }
}
