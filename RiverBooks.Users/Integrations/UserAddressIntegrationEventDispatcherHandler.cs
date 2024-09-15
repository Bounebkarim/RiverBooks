using MediatR;
using RiverBooks.Users.Contracts;
using RiverBooks.Users.Domain;
using Serilog;

namespace RiverBooks.Users.Integrations;
internal class UserAddressIntegrationEventDispatcherHandler(ILogger logger, IMediator mediator) : INotificationHandler<AddressAddedEvent>
{
  private readonly ILogger _logger = logger;
  private readonly IMediator _mediator = mediator;

  public async Task Handle(AddressAddedEvent notification, CancellationToken cancellationToken)
  {
    Guid userId = Guid.Parse(notification.Address.UserId);
    var addressDetails = new UserAddressDetails(userId,
      notification.Address.Id,
      notification.Address.StreetAddress.Street1,
      notification.Address.StreetAddress.Street2,
      notification.Address.StreetAddress.City,
      notification.Address.StreetAddress.State,
      notification.Address.StreetAddress.ZipCode,
      notification.Address.StreetAddress.Country);

    await _mediator!.Publish(new UserAddressAddedIntegrationEvent(addressDetails), cancellationToken);

    _logger.Information("[DE Handler]New address integration event sent for {user}: {address}",
      notification.Address.UserId,
      notification.Address.StreetAddress);
  }
}
