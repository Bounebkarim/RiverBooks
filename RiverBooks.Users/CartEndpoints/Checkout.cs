using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.User;
using RiverBooks.Users.UseCases.User.CheckOut;

namespace RiverBooks.Users.CartEndpoints;
internal class Checkout(IMediator mediator) : Endpoint<CheckoutRequest, CheckoutResponse>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Post("/checkout");
    Claims("email");
  }

  public override async Task HandleAsync(CheckoutRequest req, CancellationToken ct)
  {
    var emailAdress = User.FindFirstValue("email");
    if (string.IsNullOrEmpty(emailAdress))
    {
      await SendForbiddenAsync(cancellation: ct);
      return;
    }
    var command = new CheckoutCommand(emailAdress!, req.ShippingAdressId, req.BillingAdressId);
    var result = await _mediator.Send(command, ct);
    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(cancellation: ct);
      return;
    }

    await SendOkAsync(new CheckoutResponse(result.Value), cancellation: ct);
  }
}
