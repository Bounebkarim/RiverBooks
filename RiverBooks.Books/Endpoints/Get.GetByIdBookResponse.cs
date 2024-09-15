namespace RiverBooks.Books.Endpoints;

public record GetByIdBookResponse
{
  public BookDto Book { get; set; } = default!;
}
