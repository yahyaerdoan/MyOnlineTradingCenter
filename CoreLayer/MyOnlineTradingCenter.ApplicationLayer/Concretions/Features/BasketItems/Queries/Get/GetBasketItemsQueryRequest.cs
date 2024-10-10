using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Queries.Get;

public class GetBasketItemsQueryRequest : IRequest<List<GetBasketItemsQueryResponse>>
{
}
