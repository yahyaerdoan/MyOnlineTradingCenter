using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Contracts.Cofigurations;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.InfrastructureLayer.Concretions.Services.Files;

public class FileUrlGeneratorService : IFileUrlGeneratorService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly StorageSettings _storageSettings;

    public FileUrlGeneratorService(IHttpContextAccessor httpContextAccessor, IOptions<StorageSettings> storageSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        _storageSettings = storageSettings.Value;
    }

    public Response<string> GenerateFileUrl(string fileName)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
            return Response<string>.Failure("HttpContext is not available.", "Uniform resource locator generation failed", StatusCodes.Status500InternalServerError);

        var scheme = httpContext.Request.Scheme;
        var host = httpContext.Request.Host.Value;

        if (string.IsNullOrEmpty(scheme))
            return Response<string>.Failure("Invalid uniform resource locator scheme.", "Uniform resource locator generation failed", StatusCodes.Status500InternalServerError);

        if (string.IsNullOrEmpty(host))
            return Response<string>.Failure("Invalid uniform resource locator host.", "Uniform resource locator generation failed", StatusCodes.Status500InternalServerError);

        var pathToFile = _storageSettings.LocalStorageOrigin;

        if (string.IsNullOrWhiteSpace(pathToFile))
            return Response<string>.Failure("Path to file is not configured.", "Uniform resource locator generation failed", StatusCodes.Status500InternalServerError);

        var fullUrl = $"{scheme}://{host}/{pathToFile}/{fileName}";
        return Response<string>.Success(fullUrl, "Uniform resource locator generated successfully", StatusCodes.Status200OK);
    }
}

