namespace RiverBooks.Users.CartEndpoints;

public record CartResponse
{
  public List<CartItemDto> CartItems { get; set; } = new();
}
