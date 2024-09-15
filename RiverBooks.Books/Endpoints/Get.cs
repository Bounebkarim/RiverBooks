using FastEndpoints;

namespace RiverBooks.Books.Endpoints;

internal class Get(IBookService bookService) : Endpoint<GetByIdBookRequest, GetByIdBookResponse>
{
  private readonly IBookService _bookService = bookService;
  public override void Configure()
  {
    Get("/books/{id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetByIdBookRequest request, CancellationToken cancellationToken)
  {
    var book = await _bookService.GetBookByIdAsync(request.Id);
    if (book == null)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    await SendAsync(new GetByIdBookResponse()
    {
      Book = book
    }, cancellation: cancellationToken);
  }
}
