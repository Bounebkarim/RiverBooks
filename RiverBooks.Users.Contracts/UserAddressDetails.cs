namespace RiverBooks.Users.Contracts;

public record UserAddressDetails(
  Guid UserId,
  Guid Id,
  string Street1,
  string Street2,
  string City,
  string State,
  string ZipCode,
  string Country);
