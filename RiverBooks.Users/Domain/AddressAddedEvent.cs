using RiverBooks.SharedKernel;

namespace RiverBooks.Users.Domain;

public class AddressAddedEvent(UserStreetAdress address) : DomainEventBase
{
  public UserStreetAdress Address { get; } = address;
}
