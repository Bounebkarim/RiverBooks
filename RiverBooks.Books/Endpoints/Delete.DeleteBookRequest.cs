namespace RiverBooks.Books.Endpoints;

internal record DeleteBookRequest
{
  public Guid Id { get; set; }
}
