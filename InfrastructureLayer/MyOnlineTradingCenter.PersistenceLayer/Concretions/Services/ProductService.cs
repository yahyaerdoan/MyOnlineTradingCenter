using MyOnlineTradingCenter.ApplicationLayer.Abstractions;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Services
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()

            => new()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Apple",
                    Description = "IPhone 14 Pro Max",
                    Price = 1115,
                    CreatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MMM-dd hh:mm tt")),
                    UpdatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MMM-dd hh:mm tt")),
                    Stock = 10,
                    Status = true
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Apple",
                    Description = "IPhone 14 Pro Max",
                    Price = 1115,
                    CreatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MMM-dd hh:mm tt")),
                    UpdatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MMM-dd hh:mm tt")),
                    Stock = 10,
                    Status = true
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Apple",
                    Description = "IPhone 14 Pro Max",
                    Price = 1115,
                    CreatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MMM-dd hh:mm tt")),
                    UpdatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MMM-dd hh:mm tt")),
                    Stock = 10,
                    Status = true
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Apple",
                    Description = "IPhone 14 Pro Max",
                    Price = 1115,
                    CreatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MMM-dd hh:mm tt")),
                    UpdatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MMM-dd hh:mm tt")),
                    Stock = 10,
                    Status = true
                }
            };
    }
}
