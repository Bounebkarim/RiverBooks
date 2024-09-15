using MediatR;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Infrastructure;
using RiverBooks.OrderProcessing.Interfaces;
using RiverBooks.Users.Contracts;
using Serilog;

namespace RiverBooks.OrderProcessing.Integrations;
internal class AddressCacheUpdatingNewUserAddressHandler(IOrderAddressCache addressCache, ILogger logger) : INotificationHandler<UserAddressAddedIntegrationEvent>
{
  private readonly IOrderAddressCache _addressCache = addressCache;
  private readonly ILogger _logger = logger;

  public async Task Handle(UserAddressAddedIntegrationEvent notification, CancellationToken cancellationToken)
  {
    var orderAdress = new OrderAddress(notification.AddressDetails.Id,
    new Address(
      notification.AddressDetails.Street1,
      notification.AddressDetails.Street2,
      notification.AddressDetails.City,
      notification.AddressDetails.State,
      notification.AddressDetails.ZipCode,
      notification.AddressDetails.Country
    ));
    await _addressCache.StoreAsync(orderAdress, cancellationToken);
    _logger.Information("[Integration Event] Cache of User Address updated with : {address}", orderAdress);
  }
}
