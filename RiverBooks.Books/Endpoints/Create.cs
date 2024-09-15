using FastEndpoints;

namespace RiverBooks.Books.Endpoints;

internal class Create(IBookService bookService) : Endpoint<CreateBookRequest, BookDto>
{
  private readonly IBookService _bookService = bookService;
  public override void Configure()
  {
    Post("/books");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateBookRequest req, CancellationToken ct)
  {
    var newBook = new BookDto(req.Id ?? Guid.NewGuid(), req.Title, req.Author, req.Price);
    await _bookService.CreateBookAsync(newBook);
    await SendCreatedAtAsync<Get>(new { newBook.Id }, newBook, cancellation: ct);
  }
}
