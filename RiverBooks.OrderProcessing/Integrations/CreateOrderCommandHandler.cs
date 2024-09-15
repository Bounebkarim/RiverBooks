using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Contracts;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Interfaces;

namespace RiverBooks.OrderProcessing.Integrations;
internal class CreateOrderCommandHandler(
  IOrderRepository orderRepository,
  //ILogger logger,
  IOrderAddressCache addressCache)
  : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
{
  //private readonly ILogger _logger = logger;

  public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
  {
    var items = request.OrderItems.Select(o=> new OrderItem(o.BookId,o.Quantity,o.UnitPrice,o.Description)).ToList();

    //var shippingAdress = new Adress("rue abu kassim cherubic", "", "Satiety init", "Sax", "3000", "Tunisia");
    //var billingAdress = shippingAdress;

    var shippingAdress = await addressCache.GetByIdAsync(request.ShippingAdressId, cancellationToken);
    var billingAdress = await addressCache.GetByIdAsync(request.BillingAdressId, cancellationToken);

    var order = Order.Factory.CreateOrder(request.UserId, shippingAdress.Value.Adress, billingAdress.Value.Adress, items);
    await orderRepository.AddAsync(order);
    await orderRepository.SaveChangesAsync();
    return new OrderDetailsResponse(order.Id);
  }
}

