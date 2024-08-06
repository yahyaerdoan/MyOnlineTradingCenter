using MediatR;
using Microsoft.EntityFrameworkCore;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Queries.GetList;
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
        //var totalCount = _productReadRepository.GetAll(false).Count();
        //var products = _productReadRepository.GetAll(false).Skip(request.Pagination.Page * request.Pagination.Size).Take(request.Pagination.Size).Select(p=> new
        //{
        //    p.Id,
        //    p.Name,
        //    p.Description,
        //    p.Price,
        //    p.Stock,
        //    p.Status,
        //    p.CreatedDate,
        //}).ToList();

        //return new() { Products = products, TotalCount = totalCount };

        var pagination = request.Pagination ?? new Pagination();

        var query = _productReadRepository.GetAll()
            .Skip(pagination.Page * pagination.Size)
            .Take(pagination.Size);

        var products = await query.ToListAsync(cancellationToken);

        return new GetProductsQueryResponse
        {
            Products = products
        };
        //this is not return counts fix it
    }
}
