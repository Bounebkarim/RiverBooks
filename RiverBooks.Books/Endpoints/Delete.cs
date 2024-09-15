using FastEndpoints;

namespace RiverBooks.Books.Endpoints;

internal class Delete(IBookService bookService) : Endpoint<DeleteBookRequest>
{
  private readonly IBookService _bookService = bookService;
  public override void Configure()
  {
    Delete("/books/{id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(DeleteBookRequest req, CancellationToken ct)
  {
    await _bookService.DeleteBookAsync(req.Id);
    await SendNoContentAsync(cancellation: ct);
  }
}
