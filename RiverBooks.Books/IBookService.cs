namespace RiverBooks.Books;

internal interface IBookService
{
  Task<List<BookDto>> ListBooksAsync();
  Task CreateBookAsync(BookDto newBook);
  Task<BookDto?> GetBookByIdAsync(Guid id);
  Task UpdateBookPriceAsync(Guid bookId,decimal price);
  Task DeleteBookAsync(Guid bookId);
}
