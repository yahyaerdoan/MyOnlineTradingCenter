﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Queries.Get;

public class GetProductsQueryResponse
{
    public int TotalProductCount { get; set; }
    public object? Products { get; set; }
}
