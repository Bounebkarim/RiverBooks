using Ardalis.GuardClauses;

namespace RiverBooks.Users.Domain;

public class UserStreetAdress
{
  //EF
  public UserStreetAdress()
  {
  }
  public UserStreetAdress(string userId, Address streetAddress)
  {
    UserId = Guard.Against.NullOrWhiteSpace(userId);
    StreetAddress = Guard.Against.Null(streetAddress);
  }
  public Guid Id { get; set; } = Guid.NewGuid();
  public string UserId { get; set; } = string.Empty;
  public Address StreetAddress { get; set; } = default!;
}
