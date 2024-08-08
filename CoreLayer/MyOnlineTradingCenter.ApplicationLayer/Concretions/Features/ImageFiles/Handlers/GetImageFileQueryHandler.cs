using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Queries.Get;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Handlers;

public class GetImageFileQueryHandler : IRequestHandler<GetImageFileQueryRequest, List<GetImageFileQueryResponse>>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public GetImageFileQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _productReadRepository = productReadRepository;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<GetImageFileQueryResponse>> Handle(GetImageFileQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var requestScheme = httpContext.Request.Scheme;
        var requestHost = httpContext.Request.Host.Value;

        Product? product = await _productReadRepository.Table.Include(p => p.ImageFiles)
              .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
        return product.ImageFiles.Select(p => new GetImageFileQueryResponse
        {
            Id = p.Id,
            Name = p.Name,
            Status = p.Status,
            Path = $"{requestScheme}://{requestHost}/{_configuration["LocalStorageOrigin"]}/{p.Name}",
            #region Other path type           
            //Path = $"{_configuration["LocalStorageOrigin"]}/{p.Path}",
            //Path = $"{Request.Scheme}://{Request.Host}/Resource/LocalStorage/Product-Images/{p.Name}",
            #endregion

        }).ToList();
    }
}
