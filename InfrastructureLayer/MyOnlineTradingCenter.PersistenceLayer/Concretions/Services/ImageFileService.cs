using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IImageFileRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Update;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services;

public class ImageFileService : IImageFileService
{
    private readonly IImageFileWriteRepository _imageFileWriteRepository;

    public ImageFileService(IImageFileWriteRepository imageFileWriteRepository)
    {
        _imageFileWriteRepository = imageFileWriteRepository;
    }

    public async Task<Response<UpdateImageShowcaseCommandResponse>> ChangeImageShowcaseStatusAsync(UpdateImageShowcaseCommandRequest request)
    {
        if (!Guid.TryParse(request.ProductId, out var parsedProductId) || !Guid.TryParse(request.ImageId, out var parsedImageId))
        {
            return Response<UpdateImageShowcaseCommandResponse>.Failure("Error!", "Invalid ProductId or ImageId.", StatusCodes.Status400BadRequest);
        }

        var query = _imageFileWriteRepository.Table.Include(p => p.Products).SelectMany(p => p.Products, (productImageFiles, products) => new
        {
            productImageFiles,
            products
        });

        var currentShowcase = await query.FirstOrDefaultAsync(product => product.products.Id == parsedProductId && product.productImageFiles.ShowcasePicture);

        var newCurrentShowcase = await query.FirstOrDefaultAsync(product => product.productImageFiles.Id == parsedImageId);

        if (newCurrentShowcase is null)
            return Response<UpdateImageShowcaseCommandResponse>.Failure("Error!", "Either the current or new showcase image is missing.", StatusCodes.Status400BadRequest);

        if (currentShowcase is not null)
            currentShowcase.productImageFiles.ShowcasePicture = false;

        newCurrentShowcase.productImageFiles.ShowcasePicture = true;

        var updatedShowcase = newCurrentShowcase.productImageFiles.ShowcasePicture;

        await _imageFileWriteRepository.SaveAsync();

        var response = new UpdateImageShowcaseCommandResponse { ProductId = request.ProductId, ImageId = request.ImageId };
        return Response<UpdateImageShowcaseCommandResponse>.Success(response, "Showcase image updated successfully.", StatusCodes.Status200OK);
    }
}
