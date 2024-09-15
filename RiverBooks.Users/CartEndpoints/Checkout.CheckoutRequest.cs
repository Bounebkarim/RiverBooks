namespace RiverBooks.Users.CartEndpoints;

internal record CheckoutRequest
{
  public Guid ShippingAdressId { get; set; }
  public Guid BillingAdressId { get; set; }
}
