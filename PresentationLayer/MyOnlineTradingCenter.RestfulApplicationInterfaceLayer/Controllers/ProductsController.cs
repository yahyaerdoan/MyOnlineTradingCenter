using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.ICustomerRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IServices;
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
        readonly private IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository, IOrderReadRepository orderReadRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _orderReadRepository = orderReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
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
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
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
            #region File Uploading
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

            await _fileService.UploadAsync("Resource\\Product-Images", Request.Form.Files);
            return Ok(new { message = "Files uploaded successfully" });
        }
    }    
}
