namespace RiverBooks.Users.UsersEndpoints;

public record AddAddressRequest(
  string Street1,
  string Street2,
  string City,
  string State,
  string ZipCode,
  string Country);
