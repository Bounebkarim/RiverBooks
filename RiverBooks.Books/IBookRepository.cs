namespace RiverBooks.Books;

internal interface IBookRepository : IReadOnlyRepository
{
  Task AddAsync(Book book);
  Task DeleteAsync(Book book);
  Task SaveChangesAsync();
}
