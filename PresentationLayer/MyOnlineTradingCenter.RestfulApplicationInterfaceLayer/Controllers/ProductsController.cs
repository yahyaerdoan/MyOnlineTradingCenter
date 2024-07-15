using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.ICustomerRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderRepositories;
using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IProductRepositories;
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

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _orderReadRepository = orderReadRepository;
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
        public IActionResult Get()
        {
            return Ok(_productReadRepository.GetAll(false));
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(product);
        }

        [HttpPost]
        public async  Task<IActionResult> Post (CreateProductViewModel createProductViewModel)
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

            return Ok("deleted.");
        }
    }
}
