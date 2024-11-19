﻿namespace MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Products;

public class CreateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}
