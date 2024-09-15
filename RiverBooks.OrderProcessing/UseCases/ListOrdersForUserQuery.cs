using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Endpoints;
using RiverBooks.OrderProcessing.Interfaces;

namespace RiverBooks.OrderProcessing.UseCases;

internal record ListOrdersForUserQuery(string? Email) : IRequest<Result<List<OrderSummary>>>;

internal class ListOrdersForUserQueryHandler(IOrderRepository orderRepository)
  : IRequestHandler<ListOrdersForUserQuery, Result<List<OrderSummary>>>
{
  public async Task<Result<List<OrderSummary>>> Handle(ListOrdersForUserQuery request, CancellationToken cancellationToken)
  {
    var orders = await orderRepository.ListAsync();

    var summaries = orders.Select(o => new OrderSummary()
    {
      DateCreated = o.DateCreated,
      OrderId = o.Id,
      Total = o.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity),
      UserId = o.UserId
    }).ToList();
    return summaries;
  }
}
