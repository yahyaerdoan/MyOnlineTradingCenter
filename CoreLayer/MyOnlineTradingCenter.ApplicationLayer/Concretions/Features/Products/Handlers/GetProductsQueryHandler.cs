using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
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
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly IFileUrlGeneratorService _fileUrlGeneratorService;

    public GetProductsQueryHandler(IProductReadRepository productReadRepository, ILogger<GetProductsQueryHandler> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IFileUrlGeneratorService fileUrlGeneratorService)
    {
        _productReadRepository = productReadRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _fileUrlGeneratorService = fileUrlGeneratorService;
    }

    public async Task<GetProductsQueryResponse> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var requestScheme = httpContext.Request.Scheme;
        var requestHost = httpContext.Request.Host.Value;

        var query = _productReadRepository.GetAll(false).OrderBy(p => p.Price);

        var totalProductCount = await query.CountAsync(cancellationToken);

        var products = await query
            .Skip((request.Pagination.Page) * request.Pagination.Size)
            .Take(request.Pagination.Size).Include(product => product.ImageFiles)
            .Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                Status = p.Status,
                CreatedDate = p.CreatedDate,
                ImageFiles = p.ImageFiles.Select(image => new
                {
                    image.Id,
                    image.Name,
                    image.CreatedDate,
                    image.UpdatedDate,
                    image.ShowcasePicture,
                    Path = $"{requestScheme}://{requestHost}/{_configuration["LocalStorageOrigin"]}/{image.Name}"
                    //Path = _fileUrlGeneratorService.GenerateFileUrl(image.Name)
                }).ToList()

            })
            .ToListAsync(cancellationToken);
        _logger.LogInformation("Products have been listed!");
        //throw new Exception("Hata alindi!");

        return new GetProductsQueryResponse
        {
            Products = products,
            TotalProductCount = totalProductCount
        };
    }
}
