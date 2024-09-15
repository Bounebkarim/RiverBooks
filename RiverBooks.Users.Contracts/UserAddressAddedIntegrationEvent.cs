using RiverBooks.SharedKernel;

namespace RiverBooks.Users.Contracts;

public class UserAddressAddedIntegrationEvent(UserAddressDetails addressDetails) : IntegrationEventBase
{
  public UserAddressDetails AddressDetails { get; set; } = addressDetails;
} 
