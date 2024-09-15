namespace RiverBooks.Users.UsersEndpoints;

public record ListAddressesResponse
{
  public List<UserAddressDto> Addresses { get; set; } = [];
}
