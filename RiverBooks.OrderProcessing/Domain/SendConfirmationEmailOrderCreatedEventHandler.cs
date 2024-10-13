using MediatR;
using RiverBooks.EmailSending.Contracts;
using RiverBooks.Users.Contracts;

namespace RiverBooks.OrderProcessing.Domain;

internal class SendConfirmationEmailOrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
  private readonly IMediator _mediator;
  public SendConfirmationEmailOrderCreatedEventHandler(IMediator mediator) => _mediator = mediator;

  public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
  {
    var userId = notification.Order.UserId;
    var userByIdQuery = new UserDetailsByIdQuery(userId);
    var result = await _mediator.Send(userByIdQuery, cancellationToken);
    if (!result.IsSuccess)
    {
      //To do log error
      return;
    }

    var email = result.Value.Email;

    var command = new SendEmailCommand()
    {
      To = email,
      From = "noreply@test.com",
      Subject = "Order Confirmation",
      Body = $"Thank you for your order. Your ordered {notification.Order.OrderItems.Count} Items."
    };
    Guid emailId = await _mediator.Send(command, cancellationToken);

  }
}
