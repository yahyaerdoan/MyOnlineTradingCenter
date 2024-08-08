using MediatR;
using Microsoft.EntityFrameworkCore;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Queries.Get;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.RequestParameters.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Handlers;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQueryRequest, GetProductsQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetProductsQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetProductsQueryResponse> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var query = _productReadRepository.GetAll(false);

        var totalCount = await query.CountAsync(cancellationToken);

        var products = await query
            .Skip((request.Pagination.Page) * request.Pagination.Size)
            .Take(request.Pagination.Size)
            .Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                Status = p.Status,
                CreatedDate = p.CreatedDate,
            })
            .ToListAsync(cancellationToken);

        return new GetProductsQueryResponse
        {
            Products = products,
            TotalDataCount = totalCount
        };
    }
}
