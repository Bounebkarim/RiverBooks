using RiverBooks.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiverBooks.OrderProcessing.Domain;

internal class Order : IHaveDomainEvents
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public Guid UserId { get; set; }
  public Address ShippingAdress { get; set; } = default!;
  public Address BillingAdress { get; set; } = default!;
  private readonly List<OrderItem> _orderItems = [];
  public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
  public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.UtcNow;
  private readonly List<DomainEventBase> _domainEvents = [];
  [NotMapped]
  public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();
  protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
  void IHaveDomainEvents.ClearDomainEvents() => _domainEvents.Clear();
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
      var orderCreatedEvent = new OrderCreatedEvent(order);
      order.RegisterDomainEvent(orderCreatedEvent);

      return order;
    }
  }
}
