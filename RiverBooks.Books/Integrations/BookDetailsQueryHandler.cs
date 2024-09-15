using Ardalis.Result;
using MediatR;
using RiverBooks.Books.Contracts;

namespace RiverBooks.Books.Integrations;

internal class BookDetailsQueryHandler(IBookRepository bookRepository)
  : IRequestHandler<BookDetailsQuery, Result<BookDetailsResponse>>
{
  public async Task<Result<BookDetailsResponse>> Handle(BookDetailsQuery request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(request.Id);
        if (book == null)
        {
            return Result<BookDetailsResponse>.NotFound();
        }

        var response = new BookDetailsResponse(book.Id, book.Title, book.Author,book.Price);
        return Result<BookDetailsResponse>.Success(response);
    }
}
