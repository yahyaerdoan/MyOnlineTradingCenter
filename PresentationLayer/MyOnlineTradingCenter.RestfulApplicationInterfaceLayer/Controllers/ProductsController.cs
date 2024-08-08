using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.ICustomerRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IImageFileRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IInvoiceFileRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IUploadedFileRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IStorageServices.IStorageServices;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Commands.Create;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Queries.Get;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Queries.GetById;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.RequestParameters.Paginations;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.ViewModels.Products;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System.Net;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        readonly private ICustomerWriteRepository _customerWriteRepository;
        readonly private ICustomerReadRepository _customerReadRepository;

        readonly private IOrderWriteRepository _orderWriteRepository;
        readonly private IOrderReadRepository _orderReadRepository;

        readonly private IImageFileWriteRepository _imageFileWriteRepository;
        readonly private IImageFileReadRepository _imageFileReadRepository;

        readonly private IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        readonly private IInvoiceFileReadRepository _invoiceFileReadRepository;

        readonly private IUploadedFileWriteRepository _uploadedFileWriteRepository;
        readonly private IUploadedFileReadRepository _uploadedFileReadRepository;

        readonly private IWebHostEnvironment _webHostEnvironment;
        readonly private IFileService _fileService;

        readonly private IStorageService _storageService;
        private readonly IConfiguration _configuration;

        private readonly IMediator _mediator;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, ICustomerWriteRepository customerWriteRepository, ICustomerReadRepository customerReadRepository, IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, IImageFileWriteRepository imageFileWriteRepository, IImageFileReadRepository imageFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IUploadedFileWriteRepository uploadedFileWriteRepository, IUploadedFileReadRepository uploadedFileReadRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService, IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _customerWriteRepository = customerWriteRepository;
            _customerReadRepository = customerReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _imageFileWriteRepository = imageFileWriteRepository;
            _imageFileReadRepository = imageFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _uploadedFileWriteRepository = uploadedFileWriteRepository;
            _uploadedFileReadRepository = uploadedFileReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _storageService = storageService;
            _configuration = configuration;
            _mediator = mediator;
        }

        #region example

        //[HttpGet]
        //public async Task Get()
        //{
        //    #region AddRangeAsync
        //    //await _productWriteRepository.AddRangeAsync(new()
        //    //{
        //    //    new(){Id = Guid.NewGuid(), Name="product 2", Description="p2", Price = 6, CreatedDate= DateTime.UtcNow, UpdatedDate= DateTime.UtcNow, Stock = 11, Status= true}
        //    //});
        //    #endregion
        //    #region AsNoTracking
        //    //Product product = await _productReadRepository.GetByIdAsync("efe30fe6-2734-4cbd-a23c-c025ffe739a5", true);
        //    //product.Name = "yahya 3";
        //    //product.UpdatedDate = DateTime.UtcNow;
        //    //await _productWriteRepository.SaveAsync();
        //    #endregion

        //    #region CreatingDate
        //    //var customerId = Guid.NewGuid();
        //    //await _customerWriteRepository.AddAsync(new Customer() { Id = customerId, FirstName = "Yahya", LastName = "Erdogan", Email = "yahyaerdogan@gmai.com", });
        //    //await _orderWriteRepository.AddAsync(new Order() { Address = "7588 W Granville Ave", Description = "New Product", CustomerId = customerId });
        //    //await _orderWriteRepository.SaveAsync();
        //    #endregion

        //    #region UpdatingDate
        //    Order order = await _orderReadRepository.GetByIdAsync("b02e03c7-07bf-4fb9-8e38-30c0a4a06051");
        //    order.Status = true;
        //    await _orderWriteRepository.SaveAsync();
        //    #endregion


        //}
        #endregion

        #region Old Version Get Products
        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        //{

        //    var totalDataCount = await _productReadRepository.GetAll(false).CountAsync();
        //    var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
        //    {
        //        p.Id,
        //        p.Name,
        //        p.Description,
        //        p.Stock,
        //        p.Price,
        //        p.CreatedDate,
        //        p.UpdatedDate,

        //    }).ToList();
        //    return Ok(new
        //    {
        //        products,
        //        totalDataCount,
        //    });
        //}
        #endregion
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var query = new GetProductsQueryRequest(pagination);
            GetProductsQueryResponse response = await _mediator.Send(query);
            return Ok(response);

        }

        #region old version get by id
        //[HttpGet("id")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    Product product = await _productReadRepository.GetByIdAsync(id, false);
        //    return Ok(product);
        //}
        #endregion
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest request)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        #region Old Version Create Product
        //[HttpPost]
        //public async Task<IActionResult> Post(CreateProductViewModel createProductViewModel)
        //{
        //    await _productWriteRepository.AddAsync(new()
        //    {
        //        Name = createProductViewModel.Name,
        //        Description = createProductViewModel.Description,
        //        Stock = createProductViewModel.Stock,
        //        Price = createProductViewModel.Price,
        //    });
        //    await _productWriteRepository.SaveAsync();
        //    return StatusCode((int)HttpStatusCode.Created);
        //}
        #endregion
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductViewModel updateProductViewModel)
        {
            Product product = await _productReadRepository.GetByIdAsync(updateProductViewModel.Id);

            product.Name = updateProductViewModel.Name;
            product.Description = updateProductViewModel.Description;
            product.Stock = updateProductViewModel.Stock;
            product.Price = updateProductViewModel.Price;

            await _productWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveByIdAsync(id);

            await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            #region File Uploading Only for Controller
            //string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Resource/Product-Images");

            //if (!Directory.Exists(uploadPath))
            //{
            //    Directory.CreateDirectory(uploadPath);

            //}

            //foreach (var file in Request.Form.Files)
            //{
            //    string uniqueFilename = Path.GetFileNameWithoutExtension(file.Name)+ "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.Name);

            //    string fullPath = Path.Combine(uploadPath, uniqueFilename);

            //    using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: false);
            //    await file.CopyToAsync(stream);
            //    await stream.FlushAsync();
            //}

            //return Ok(new { message = "Files uploaded successfully" });

            #endregion

            #region File Uploading Only for File Service
            //var datas = await _fileService.UploadAsync("Resource\\Product-Images", Request.Form.Files);

            //await _imageFileWriteRepository.AddRangeAsync(datas.Select(d => new ImageFile()
            //{
            //    Name = d.FileName,
            //    Path = d.TargetFolderPath,

            //}).ToList());
            //await _imageFileWriteRepository.SaveAsync();

            //return Ok(new { message = "Files uploaded successfully" });
            #endregion

            #region File Uploading to Storage

            //var datas = await _storageService.UploadAsync("Resource\\LocalStorage\\Product-Images", Request.Form.Files);

            //await _imageFileWriteRepository.AddRangeAsync(datas.Select(d => new ImageFile()
            //{
            //    Name = d.FileName,
            //    Path = d.TargetFolderPathOrContainerName,
            //    Storage = _storageService.StorageName,

            //}).ToList());
            //await _imageFileWriteRepository.SaveAsync();

            //return Ok(new { message = "Files uploaded successfully" });

            #endregion

            List<(string FileName, string FileExtension, string FullPath, string TargetFolderPathOrContainerName)> result = await _storageService.UploadAsync("Resource/LocalStorage/Product-Images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);

            #region Option 2

            //foreach (var r in result)
            //{
            //    product.ImageFiles.Add(new()
            //    {
            //        Name = x.FileName,
            //        Path = $"{x.TargetFolderPathOrContainerName}/{x.FileName}",
            //        Storage = _storageService.StorageName,
            //        Products = new List<Product> { product }
            //    });
            //}

            #endregion

            await _imageFileWriteRepository.AddRangeAsync(result.Select(x => new ImageFile
            {
                Name = x.FileName,
                Path = $"{x.TargetFolderPathOrContainerName}/{x.FileName}",
                Storage = _storageService.StorageName,
                Products = new List<Product> { product }

            }).ToList());
            await _imageFileWriteRepository.SaveAsync();
            return Ok(new { message = "Files uploaded successfully" });
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetImages(string id)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ImageFiles)
                  .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            // await Task.Delay(2000);
            return Ok(product.ImageFiles.Select(p => new
            {
                p.Id,
                p.Name,
                p.Status,
                Path = $"{Request.Scheme}://{Request.Host}/{_configuration["LocalStorageOrigin"]}/{p.Name}",
                #region Other path type
                //p.Path,
                //Path = $"{_configuration["LocalStorageOrigin"]}/{p.Path}",
                //Path = $"{Request.Scheme}://{Request.Host}/Resource/LocalStorage/Product-Images/{p.Name}",
                #endregion
            }));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteImage(string id, string imageId)
        {
            var productIdGuid = Guid.Parse(id); var imageIdGuid = Guid.Parse(imageId);

            var product = await _productReadRepository.Table.Include(p => p.ImageFiles)
                .FirstOrDefaultAsync(p => p.Id == productIdGuid);

            if (product == null)
                return NotFound(new { message = "Product not found" });

            var imageFile = product.ImageFiles.FirstOrDefault(p => p.Id == imageIdGuid);
            if (imageFile == null)
                return NotFound(new { message = "Image not found" });

            var uploadedFile = await _imageFileReadRepository.Table.FirstOrDefaultAsync(p => p.Id == imageIdGuid);
            if (uploadedFile == null)
                return NotFound(new { message = "Uploaded file record not found" });

            await _uploadedFileWriteRepository.RemoveAsync(uploadedFile);
            await _uploadedFileWriteRepository.SaveAsync();

            product.ImageFiles.Remove(imageFile);
            await _imageFileWriteRepository.SaveAsync();

            //string filePath = Path.Combine("wwwroot/Resource/LocalStorage/Product-Images", imageFile.Name);

            string filePath = Path.Combine($"wwwroot/{_configuration["LocalStorageOrigin"]}/", imageFile.Name);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            return Ok(new { message = "Files deleted successfully" });
        }
    }
}
