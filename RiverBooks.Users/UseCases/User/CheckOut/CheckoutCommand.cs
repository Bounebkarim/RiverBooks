using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Contracts;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.User.CheckOut;

internal record CheckoutCommand(string EmailAdress, Guid ShippingAdressId, Guid BillingAdressId)
  : IRequest<Result<Guid>>;

internal
  class CheckoutCommandHandler(IApplicationUserRepository userRepository, IMediator mediator)
  : IRequestHandler<CheckoutCommand, Result<Guid>>
{
  private readonly IMediator _mediator = mediator;

  public async Task<Result<Guid>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
  {
    var user = await userRepository.GetUserWithCartByEmailAsync(request.EmailAdress);
    if (user is null)
    {
      return Result<Guid>.Unauthorized();
    }
    var items = user.CartItems.Select(o => new OrderItemsDetails(o.BookId, o.Quantity, o.UnitePrice, o.Description)).ToList();
    var command = new CreateOrderCommand(Guid.Parse(user.Id), request.ShippingAdressId, request.BillingAdressId, items);
    var result = await _mediator.Send(command, cancellationToken);
    if (!result.IsSuccess)
    {
      return Result<Guid>.Unauthorized();
    }
    user.ClearCart();
    await userRepository.SaveChangesAsync();
    return result.Map(o => o.OrderId);
  }
}
