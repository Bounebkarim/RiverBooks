using FastEndpoints;

namespace RiverBooks.Books.Endpoints;

internal class Update(IBookService bookService) : Endpoint<UpdateBookPriceRequest>
{
  private readonly IBookService _bookService = bookService;
  public override void Configure()
  {
    Put("/books/{id}/pricehistory");
    AllowAnonymous();
  }

  public override async Task HandleAsync(UpdateBookPriceRequest req, CancellationToken ct)
  {
    await _bookService.UpdateBookPriceAsync(req.Id, req.NewPrice);
    var book = await _bookService.GetBookByIdAsync(req.Id);
    await SendAsync(book, cancellation: ct);
  }
}
