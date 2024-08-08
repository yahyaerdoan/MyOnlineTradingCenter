using MediatR;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IImageFileRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IStorageServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Commands.Create;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.ImageFiles.Handlers;

public class CreateImageFileCommandHandler : IRequestHandler<CreateImageFileCommandRequest, CreateImageFileCommandResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IImageFileWriteRepository _imageFileWriteRepository;
    private readonly IStorageService _storageService;

    public CreateImageFileCommandHandler(IProductReadRepository productReadRepository, IImageFileWriteRepository imageFileWriteRepository, IStorageService storageService)
    {
        _productReadRepository = productReadRepository;
        _imageFileWriteRepository = imageFileWriteRepository;
        _storageService = storageService;
    }

    public async Task<CreateImageFileCommandResponse> Handle(CreateImageFileCommandRequest request, CancellationToken cancellationToken)
    {
        List<(string FileName, string FileExtension, string FullPath, string TargetFolderPathOrContainerName)> result = await _storageService.UploadAsync("Resource/LocalStorage/Product-Images", request.Files);

        Product product = await _productReadRepository.GetByIdAsync(request.Id);
        await _imageFileWriteRepository.AddRangeAsync(result.Select(x => new ImageFile
        {
            Name = x.FileName,
            Path = $"{x.TargetFolderPathOrContainerName}/{x.FileName}",
            Storage = _storageService.StorageName,
            Products = new List<Product> { product }

        }).ToList());
        await _imageFileWriteRepository.SaveAsync();

        return new();
    }
}
