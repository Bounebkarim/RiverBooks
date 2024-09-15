using Ardalis.GuardClauses;

namespace RiverBooks.Books;

internal class BookService : IBookService
{
  private readonly IBookRepository _bookRepository;

  public BookService(IBookRepository bookRepository)
  {
    _bookRepository = bookRepository;
  }
    public async Task<List<BookDto>> ListBooksAsync()
    {
      var books = (await _bookRepository.ListAsync()).Select(book => new BookDto(book.Id,book.Title,book.Author,book.Price)).ToList();
      return books;
    }

    public async Task CreateBookAsync(BookDto newBook)
    {
      var book = new Book(newBook.Id,newBook.Title,newBook.Author,newBook.Price);
      await _bookRepository.AddAsync(book);
      await _bookRepository.SaveChangesAsync();
    }

    public async Task<BookDto?> GetBookByIdAsync(Guid id)
    {
      var book = await _bookRepository.GetByIdAsync(id);
      return book == null ? null : new BookDto(book.Id,book.Title,book.Author,book.Price);
    }

    public async Task UpdateBookPriceAsync(Guid bookId, decimal price)
    {
      var book = await _bookRepository.GetByIdAsync(bookId);
      book?.UpdatePrice(price);
      await _bookRepository.SaveChangesAsync();
  }

    public async Task DeleteBookAsync(Guid bookId)
    {
      var book = await _bookRepository.GetByIdAsync(bookId);
      await _bookRepository.DeleteAsync(Guard.Against.Null(book));
      await _bookRepository.SaveChangesAsync();
  }
}
