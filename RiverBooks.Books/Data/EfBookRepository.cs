using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books.Data;

internal class EfBookRepository : IBookRepository
{
  private readonly BookDbContext _bookDbContext;

  public EfBookRepository(BookDbContext bookDbContext)
  {
    _bookDbContext = bookDbContext;
  }
  public async Task<List<Book>> ListAsync()
  {
    return await _bookDbContext.Books.ToListAsync();
  }

  public async Task<Book?> GetByIdAsync(Guid id)
  {
    return await _bookDbContext.Books.FindAsync(id);
  }

  public Task AddAsync(Book book)
  {
    _bookDbContext.Add(book);
    return Task.CompletedTask;
  }

  public Task DeleteAsync(Book book)
  {
    _bookDbContext.Remove(book);
    return Task.CompletedTask;
  }

  public async Task SaveChangesAsync()
  {
    await _bookDbContext.SaveChangesAsync();
  }
}
