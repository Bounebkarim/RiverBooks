namespace RiverBooks.OrderProcessing.Domain;

internal class Order
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public Guid UserId { get; set; }
  public Address ShippingAdress { get; set; } = default!;
  public Address BillingAdress { get; set; } = default!;
  private readonly List<OrderItem> _orderItems = [];
  public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
  public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.UtcNow;
  public void AddOrderItem(OrderItem orderItem) => _orderItems.Add(orderItem);

  internal class Factory
  {
    public static Order CreateOrder(Guid userId, Address shippingAdress, Address billingAdress, List<OrderItem> orderItems)
    {
      var order = new Order()
      {
        UserId = userId,
        ShippingAdress = shippingAdress,
        BillingAdress = billingAdress
      };
      foreach (var orderItem in orderItems)
      {
        order.AddOrderItem(orderItem);
      }
      return order;
    }
  }
}


