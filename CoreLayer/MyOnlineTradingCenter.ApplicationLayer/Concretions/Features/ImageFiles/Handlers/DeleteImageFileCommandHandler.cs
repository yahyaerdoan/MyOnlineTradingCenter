using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IImageFileRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IUploadedFileRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Handlers;

public class DeleteImageFileCommandHandler : IRequestHandler<DeleteImageFileCommandRequest, DeleteImageFileCommandResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IImageFileReadRepository _imageFileReadRepository;
    private readonly IImageFileWriteRepository _imageFileWriteRepository;
    private readonly IUploadedFileWriteRepository _uploadedFileWriteRepository;
    private readonly IConfiguration _configuration;

    public DeleteImageFileCommandHandler(IProductReadRepository productReadRepository, IImageFileReadRepository imageFileReadRepository, IImageFileWriteRepository imageFileWriteRepository, IUploadedFileWriteRepository uploadedFileWriteRepository, IConfiguration configuration)
    {
        _productReadRepository = productReadRepository;
        _imageFileReadRepository = imageFileReadRepository;
        _imageFileWriteRepository = imageFileWriteRepository;
        _uploadedFileWriteRepository = uploadedFileWriteRepository;
        _configuration = configuration;
    }

    public async Task<DeleteImageFileCommandResponse> Handle(DeleteImageFileCommandRequest request, CancellationToken cancellationToken)
    {
        var productIdGuid = Guid.Parse(request.Id); var imageIdGuid = Guid.Parse(request.ImageId);
        var product = await _productReadRepository.Table.Include(p => p.ImageFiles)
            .FirstOrDefaultAsync(p => p.Id == productIdGuid);     

        var imageFile = product?.ImageFiles.FirstOrDefault(p => p.Id == imageIdGuid);     

        var uploadedFile = await _imageFileReadRepository.Table.FirstOrDefaultAsync(p => p.Id == imageIdGuid);      

        await _uploadedFileWriteRepository.RemoveAsync(uploadedFile);
        await _uploadedFileWriteRepository.SaveAsync();

        product?.ImageFiles.Remove(imageFile);
        await _imageFileWriteRepository.SaveAsync();

        //string filePath = Path.Combine("wwwroot/Resource/LocalStorage/Product-Images", imageFile.Name);

        string filePath = Path.Combine($"wwwroot/{_configuration["LocalStorageOrigin"]}/", imageFile.Name);
        if (System.IO.File.Exists(filePath))
            System.IO.File.Delete(filePath);
        return new();
    }
}
