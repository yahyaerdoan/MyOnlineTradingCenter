namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Queries.GetById;

public class GetByIdProductQueryResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}
