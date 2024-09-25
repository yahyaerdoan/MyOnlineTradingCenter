using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<GetProductsQueryHandler> _logger;

    public GetProductsQueryHandler(IProductReadRepository productReadRepository, ILogger<GetProductsQueryHandler> logger)
    {
        _productReadRepository = productReadRepository;
        _logger = logger;
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
        _logger.LogInformation("Products have been listed!");
        throw new Exception("Hata alindi!");

        return new GetProductsQueryResponse
        {
            Products = products,
            TotalDataCount = totalCount
        };
    }
}
