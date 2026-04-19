namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.BasketItems.Queries.Get;

public class GetBasketItemsQueryResponse
{
    public string BasketItemId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
