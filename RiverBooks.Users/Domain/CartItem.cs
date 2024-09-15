using Ardalis.GuardClauses;

namespace RiverBooks.Users.Domain;

public class CartItem
{
  public CartItem()
  {
    //EF ctor
  }
  public CartItem(Guid bookId, string description, int quantity, decimal unitePrice)
  {
    BookId = Guard.Against.Default(bookId);
    Quantity = Guard.Against.Negative(quantity);
    UnitePrice = Guard.Against.Negative(unitePrice);
    Description = Guard.Against.NullOrEmpty(description);
  }
  public Guid Id { get; set; } = Guid.NewGuid();
  public Guid BookId { get; set; }
  public int Quantity { get; set; }
  public decimal UnitePrice { get; set; }
  public string Description { get; set; } = string.Empty;
  internal void UpdateQuantity(int quantity)
  {
    Quantity = Guard.Against.Negative(quantity);
  }
  internal void UpdateDescription(string description)
  {
    Description = Guard.Against.NullOrWhiteSpace(description);
  }
  internal void UpdatePrice(decimal price)
  {
    UnitePrice = Guard.Against.Negative(price);
  }

}
