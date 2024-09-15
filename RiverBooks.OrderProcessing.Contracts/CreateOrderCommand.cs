using Ardalis.Result;
using MediatR;

namespace RiverBooks.OrderProcessing.Contracts;

public record CreateOrderCommand(
  Guid UserId,
  Guid ShippingAdressId,
  Guid BillingAdressId,
  List<OrderItemsDetails> OrderItems) : IRequest<Result<OrderDetailsResponse>>;
