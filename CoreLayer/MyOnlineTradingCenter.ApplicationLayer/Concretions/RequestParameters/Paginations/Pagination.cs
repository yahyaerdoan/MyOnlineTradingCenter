namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.RequestParameters.Paginations;

public record Pagination
{
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 5;
}
