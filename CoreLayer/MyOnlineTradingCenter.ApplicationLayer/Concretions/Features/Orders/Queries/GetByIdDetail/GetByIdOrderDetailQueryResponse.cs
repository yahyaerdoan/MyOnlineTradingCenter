using MyOnlineTradingCenter.DataTransferObjectLayer.Concretions.DataTransferObjects.Orders;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Queries.GetByIdDetail;

public class GetByIdOrderDetailQueryResponse
{
    public OrderDetailDto OrderDetailDto { get; set; } = new OrderDetailDto();
}
