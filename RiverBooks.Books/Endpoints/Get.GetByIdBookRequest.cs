namespace RiverBooks.Books.Endpoints;

public record GetByIdBookRequest
{
  public Guid Id { get; set; }
}
