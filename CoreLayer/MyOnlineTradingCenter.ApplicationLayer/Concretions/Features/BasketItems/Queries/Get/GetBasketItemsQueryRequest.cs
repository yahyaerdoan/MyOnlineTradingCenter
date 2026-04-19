using MediatR;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Queries.Get;

public class GetBasketItemsQueryRequest : IRequest<List<GetBasketItemsQueryResponse>>
{
}
