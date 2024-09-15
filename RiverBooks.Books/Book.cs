using Ardalis.GuardClauses;

namespace RiverBooks.Books;

internal class Book(Guid id, string title, string author, decimal price)
{
  public Guid Id { get; set; } = Guard.Against.Default(id);
  public string Title { get; set; } = Guard.Against.NullOrEmpty(title);
  public string Author { get; set; } = Guard.Against.NullOrEmpty(author);
  public decimal Price { get; set; } = Guard.Against.Negative(price);

  internal void UpdatePrice(decimal newPrice)
  {
    Price = Guard.Against.Negative(newPrice);
  }
}
