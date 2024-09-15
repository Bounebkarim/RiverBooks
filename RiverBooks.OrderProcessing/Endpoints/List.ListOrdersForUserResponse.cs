namespace RiverBooks.OrderProcessing.Endpoints;

internal record ListOrdersForUserResponse
{
  public List<OrderSummary>? Orders { get; set; }
}
