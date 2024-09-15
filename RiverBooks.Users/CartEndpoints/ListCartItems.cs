using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases;
using RiverBooks.Users.UseCases.Cart;

namespace RiverBooks.Users.CartEndpoints;
public class ListCartItems(IMediator mediator) : EndpointWithoutRequest<CartResponse>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Get("/cart");
    Claims("email");
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var emailAdress = User.FindFirstValue("email");
    if (string.IsNullOrEmpty(emailAdress))
    {
      await SendForbiddenAsync(cancellation: ct);
      return;
    }
    var query = new ListCartItemsQuery(emailAdress);
    var result = await _mediator.Send(query, ct);
    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(cancellation: ct);
      return;
    }
    var response = new CartResponse()
    {
      CartItems = result.Value
    };
    await SendOkAsync(response, cancellation: ct);
  }
}
