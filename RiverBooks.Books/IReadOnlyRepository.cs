namespace RiverBooks.Books;

internal interface IReadOnlyRepository
{
  Task<List<Book>> ListAsync();
  Task<Book?> GetByIdAsync(Guid id);
}
