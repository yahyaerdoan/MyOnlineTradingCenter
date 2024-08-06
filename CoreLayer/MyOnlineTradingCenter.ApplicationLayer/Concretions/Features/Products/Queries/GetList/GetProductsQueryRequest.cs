using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.RequestParameters.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Queries.GetList;

public class GetProductsQueryRequest : IRequest<GetProductsQueryResponse>
{
    public Pagination? Pagination { get; set; }
}
