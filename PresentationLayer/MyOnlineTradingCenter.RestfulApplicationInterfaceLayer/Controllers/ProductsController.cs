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
        readonly private IInvoiceFileReadRepository _invoicesFileReadRepository;

        readonly private IWebHostEnvironment _webHostEnvironment;
        readonly private IFileService _fileService;

        readonly private IStorageService _storageService;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, ICustomerWriteRepository customerWriteRepository, ICustomerReadRepository customerReadRepository, IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, IImageFileWriteRepository imageFileWriteRepository, IImageFileReadRepository imageFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IUploadedFileWriteRepository uploadedFileWriteRepository, IInvoiceFileReadRepository invoicesFileReadRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService, IStorageService storageService)
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
            _invoicesFileReadRepository = invoicesFileReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _storageService = storageService;
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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalDataCount = await _productReadRepository.GetAll(false).CountAsync();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate,

            }).ToList();
            return Ok(new
            {
                products,
                totalDataCount,
            });
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductViewModel createProductViewModel)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = createProductViewModel.Name,
                Description = createProductViewModel.Description,
                Stock = createProductViewModel.Stock,
                Price = createProductViewModel.Price,
            });
            await _productWriteRepository.SaveAsync();
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
        public async Task<IActionResult> Upload()
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

            var datas = await _storageService.UploadAsync("Resource\\LocalStorage\\Product-Images", Request.Form.Files);

           await _imageFileWriteRepository.AddRangeAsync(datas.Select(d => new ImageFile()
            {
               Name = d.FileName,
               Path = d.TargetFolderPathOrContainerName,
               Storage = _storageService.StorageName,

            }).ToList());
            await _imageFileWriteRepository.SaveAsync();

            return Ok(new { message = "Files uploaded successfully" });
        }
    }
}
