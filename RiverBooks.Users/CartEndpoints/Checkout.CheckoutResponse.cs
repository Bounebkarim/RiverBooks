namespace RiverBooks.Users.CartEndpoints;

internal record CheckoutResponse(Guid OrderId)
{
  public Guid OrderId { get; set; } = OrderId;
}
