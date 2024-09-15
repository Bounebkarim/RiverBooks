namespace RiverBooks.Books.Endpoints;

internal record UpdateBookPriceRequest
{
  public Guid Id { get; set; }
  public decimal NewPrice { get; set; }
}
