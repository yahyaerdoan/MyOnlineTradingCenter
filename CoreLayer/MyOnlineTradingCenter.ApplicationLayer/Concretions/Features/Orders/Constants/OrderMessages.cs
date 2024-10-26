namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Orders.Constants;

public class OrderMessages
{
    public const string PropertiesCannotBeEmpty = "Address and description cannot be empty.";
    public const string UserMustBeLoggedIn = "User must be logged in to create an order.";
    public const string NoItemsInBasket = "No items in basket to create an order.";
    public const string CreatingFailed = "Creating order failed.";
    public const string NewOrderCreated = "New order created successfully.";
    public const string OrderCreated = "Order created successfully.";
}
