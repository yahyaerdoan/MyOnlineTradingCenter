﻿using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<ImageFile> ImageFiles { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }
}
