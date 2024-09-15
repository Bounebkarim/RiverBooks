namespace RiverBooks.Books.Endpoints;

public record ListBooksResponse
{
  public List<BookDto>? Books { get; set; }
}
