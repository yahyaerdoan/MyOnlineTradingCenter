﻿using MyOnlineTradingCenter.ApplicationLayer.Abstractions.IGenericRepositories;
using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Abstractions.IRepositories.IOrderItemRepositories;

public interface IOrderItemReadRepository : IReadRepository<OrderItem>
{
}
